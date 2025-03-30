using ElmahCore.Mvc;
using MediatR;
using MeuAtendimento.API.Configurations;
using MeuAtendimento.Domain.Core.Settings;
using MeuAtendimento.Domain.Core.Types;
using MeuAtendimento.Infra.CrossCutting.IoC;
using MeuAtendimento.Services.API.Configurations;
using MeuAtendmento.Infra.CrossCutting.ExceptionHandler.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace MeuAtendimento.API
{
    public class Startup
    {
        #region Constantes Publicas

        public const string ELMAH_BASE_PATH = "/log-errors";

        #endregion Constantes Publicas

        #region Construtores Publicos

        public Startup(IWebHostEnvironment env)
        {
            IConfigurationBuilder _builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                                                                       .AddJsonFile(path: "appsettings.json",
                                                                                    optional: true,
                                                                                    reloadOnChange: true)
                                                                       .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json",
                                                                                    optional: true,
                                                                                    reloadOnChange: true);

            if (env.IsDevelopment())
            {
                Assembly _appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));

                if (_appAssembly != null)
                    _builder.AddUserSecrets(assembly: _appAssembly,
                                            optional: true);
            }

            _builder.AddEnvironmentVariables();
            Configuration = _builder.Build();
        }

        #endregion Construtores Publicos

        #region Propriedades Publicas

        public IConfiguration Configuration { get; }

        #endregion Propriedades Publicas

        #region Metodos Publicos

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region CultureInfo

            // Configuração de cultura global para pt-BR
            CultureInfo _cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = _cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = _cultureInfo;

            #endregion CultureInfo

            services.AddControllers(options =>
            {
                options.UseCentralRoutePrefix(new RouteAttribute("api/v{version:apiVersion}"));
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.UseCamelCasing(true);
            });

            int? _apiVersion = Configuration.GetSection(nameof(ApplicationSettings))?
                                            .Get<ApplicationSettings>()?.ApiVersion;

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(_apiVersion ?? 1, 0);
                options.UseApiBehavior = false;
                options.ErrorResponses = new ApiVersionExceptionHandler();
            });

            services.AddAutoMapperSetup();

            services.AddHttpClient();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "MeuAtendimento API",
                        Version = "v1",
                        Description = "API REST desenvolvida com ASP .NET Core 3.1 para a aplicação <b>Meu Atendimento</b>.",
                        Contact = new OpenApiContact
                        {
                            Name = "Allan Carvalho Barbosa",
                            Email = "allancbarbosa@gmail.com",
                            Url = new Uri("https://github.com/Allcb")
                        }
                    });

                options.IncludeXmlComments(Path.Combine("wwwroot", "api-docs.xml"));
            });

            services.AddHttpContextAccessor();

            services.AddMediatR(typeof(Startup));
            services.AddElmah(options => options.Path = ELMAH_BASE_PATH);
            RegisterServices(services);
            RegisterSettings(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseExceptionHandlerMiddleware();

            app.UseSwagger()
               .UseSwaggerUI(options =>
               {
                   options.RoutePrefix = "api/v1/documentation";

                   options.SwaggerEndpoint(url: "../../../swagger/v1/swagger.json",
                                           name: "Documentação API v1");

                   options.DocumentTitle = "MeuAtendimento API - Swagger UI";

                   options.DisplayRequestDuration();

                   options.DocExpansion(DocExpansion.None);
               });

            app.UseCors(corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyHeader();
                corsPolicyBuilder.AllowAnyMethod();
                corsPolicyBuilder.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseWhen(context => context.Request.Path.StartsWithSegments(ELMAH_BASE_PATH, StringComparison.OrdinalIgnoreCase),
                appBuilder => appBuilder.Use(next =>
                {
                    return async context =>
                    {
                        context.Features.Get<IHttpBodyControlFeature>().AllowSynchronousIO = true;

                        await next(context);
                    };
                }));

            app.UseElmah();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #endregion Metodos Publicos

        #region Metodos Privados

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());
            NativeInjectorMeuAtendimento.RegisterServices(services);
        }

        private void RegisterSettings(IServiceCollection services)
        {
            services.Configure<TokenConfigurationsSettings>(Configuration.GetSection(nameof(TokenConfigurationsSettings)));
            services.Configure<ConnectionStringsTypes>(Configuration.GetSection(nameof(ConnectionStringsTypes)));
            services.Configure<ApplicationSettings>(Configuration.GetSection(nameof(ApplicationSettings)));
        }

        #endregion Metodos Privados
    }
}