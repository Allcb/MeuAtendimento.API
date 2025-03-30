using MeuAtendimento.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MeuAtendimento.Services.API.Configurations
{
    public static class AutoMapperSetup
    {
        #region Metodos Publicos

        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();
        }

        #endregion Metodos Publicos
    }
}