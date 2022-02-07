using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using MandradeFrameworks.SharedKernel.Extensions;
using MandradeFrameworks.Tests.Models;
using System;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;

namespace MandradeFrameworks.Tests.Integration
{
    public class ApplicationTests<TStartup, TContext> : WebApplicationFactory<TStartup>
    where TStartup : class
    where TContext : DbContext
    {
        public ConfiguracoesTestes Configuracoes { get; private set; }

        private string _nomeInstanciaBanco;
        private string _connectionStringProcessada;
        private Func<TContext, Task> _configuracoesDb;

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return base.CreateWebHostBuilder()
                .UseEnvironment("Tests");
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                RemoverConfigsDbContext(services);

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlServer()
                    .BuildServiceProvider();

                using var scope = services.BuildServiceProvider().CreateScope();
                    AdicionarDbContext(scope, services);
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connectionStringProcessada = _connectionStringProcessada.Replace(_nomeInstanciaBanco, "master");
            using var conexao = new SqlConnection(_connectionStringProcessada);
            conexao.Execute(QueryDisposeDb());
        }

        internal void AdicionarSetupBanco(Func<TContext, Task> setup)
            => _configuracoesDb = setup;

        private void RemoverConfigsDbContext(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TContext>));
            if (descriptor != null)
                services.Remove(descriptor);
        }

        private void AdicionarDbContext(IServiceScope scope, IServiceCollection services)
        {
            Configuracoes = scope.ServiceProvider.ObterServico<ConfiguracoesTestes>();
            GerarDatabaseConnectionString();
            
            services.AddDbContext<TContext>(options => options.UseSqlServer(_connectionStringProcessada));

            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Database.Migrate();
            _configuracoesDb(context);
        }

        private void GerarDatabaseConnectionString()
        {
            _nomeInstanciaBanco = Guid.NewGuid().ToString().Replace("-", "");
            string sqlQuery = $"CREATE DATABASE {_nomeInstanciaBanco}";

            using var conexao = new SqlConnection(Configuracoes.ConnectionString);
            conexao.Execute(sqlQuery);
            _connectionStringProcessada = Configuracoes.ConnectionString.Replace("master", _nomeInstanciaBanco);
        }

        private string QueryDisposeDb() =>
            $@" DECLARE @PROCESSOS_EXECUTAR varchar(1000) = '';  
                SELECT @PROCESSOS_EXECUTAR = @PROCESSOS_EXECUTAR + 'KILL ' + CONVERT(varchar(5), session_id) + ';'  
                FROM sys.dm_exec_sessions
                WHERE database_id  = db_id('{_nomeInstanciaBanco}')
                EXEC (@PROCESSOS_EXECUTAR)
                DROP DATABASE {_nomeInstanciaBanco}";
    }
}