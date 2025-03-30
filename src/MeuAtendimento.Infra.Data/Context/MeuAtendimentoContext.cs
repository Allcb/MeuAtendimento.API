using MeuAtendimento.Domain.Core.Models;
using MeuAtendimento.Infra.Data.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MeuAtendimento.Infra.Data.Context
{
    public class MeuAtendimentoContext : DbContext
    {
        #region Campos Privados

        private ILoggerFactory _loggerFactory;

        #endregion Campos Privados

        #region Construtores Publicos

        public MeuAtendimentoContext()
        {
        }

        public MeuAtendimentoContext(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        #endregion Construtores Publicos

        #region Propriedades Publicas

        //TODO
        //public DbSet<Paciente> Pacientes { get; set; }
        //public DbSet<Atendimento> Atendimentos { get; set; }
        //public DbSet<Triagem> Triagens { get; set; }

        #endregion Propriedades Publicas

        #region Metodos Protegidos

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuAtendimentoContext).Assembly);
            modelBuilder.ApplyGlobalStandards();
            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                          .AddJsonFile(path: "appsettings.json",
                                                                                       optional: true)
                                                                          .AddJsonFile(path: $"appsettings.{_environment}.json",
                                                                                       optional: true,
                                                                                       reloadOnChange: true)
                                                                          .AddEnvironmentVariables()
                                                                          .Build();

            if (Debugger.IsAttached)
                optionsBuilder.UseLoggerFactory(_loggerFactory);

            if (_environment.Equals("Test"))
                optionsBuilder.UseInMemoryDatabase(databaseName: "MeuAtendimento");
            else if (_environment.Equals("Development") && Environment.GetCommandLineArgs().Any(arg => arg.Contains("ef")))
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString(nameof(MeuAtendimentoContext)))
                              .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            else
                optionsBuilder.UseSqlServer(GetConnectionStringFromEnvironment())
                              .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        #endregion Metodos Protegidos

        #region Metodos Publicos

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess: acceptAllChangesOnSuccess,
                                         cancellationToken: cancellationToken);
        }

        #endregion Metodos Publicos

        #region Metodos Privados

        private void OnBeforeSaving()
        {
            ChangeTracker.Entries()
                         .ToList()
                         .ForEach(entry =>
                         {
                             if (entry.Entity is Entity trackableEntity)
                             {
                                 if (entry.State == EntityState.Added)
                                 {
                                     trackableEntity.CreatedDate = DateTime.Now;
                                     trackableEntity.IsDeleted = false;
                                 }
                                 else if (entry.State == EntityState.Modified)
                                     trackableEntity.ModifiedDate = DateTime.Now;
                             }
                         });
        }

        public static string GetConnectionStringFromEnvironment()
        {
            IDictionary _variaveisAmbiente = Environment.GetEnvironmentVariables();

            SqlConnectionStringBuilder _connectionStringBuilder = new SqlConnectionStringBuilder();

            _connectionStringBuilder.DataSource = _variaveisAmbiente["SERVIDOR"]?.ToString();
            _connectionStringBuilder.InitialCatalog = _variaveisAmbiente["NOME_BANCO"]?.ToString();
            _connectionStringBuilder.IntegratedSecurity = true;
            _connectionStringBuilder.PersistSecurityInfo = false;
            _connectionStringBuilder.UserID = _variaveisAmbiente["LOGIN"]?.ToString();
            _connectionStringBuilder.Password = _variaveisAmbiente["SENHA"]?.ToString();
            _connectionStringBuilder.MultipleActiveResultSets = false;
            _connectionStringBuilder.Encrypt = false;
            _connectionStringBuilder.TrustServerCertificate = false;
            _connectionStringBuilder.Add(keyword: "Trusted_Connection", value: false);
            _connectionStringBuilder.Pooling = true;
            _connectionStringBuilder.MaxPoolSize = 5000;

            return _connectionStringBuilder.ConnectionString;
        }

        #endregion Metodos Privados
    }
}