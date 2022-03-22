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
        private string _nomeInstanciaBanco;
        private string _connectionStringMaster;
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
                string cnn = "Server=DEV\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
                var configs = new ConfiguracoesTestes(cnn);

                GerarDatabaseConnectionString(configs);

                var provider = services.BuildServiceProvider();
                using var scope = provider.CreateScope();
                {
                    var context = scope.ServiceProvider.GetRequiredService<TContext>();
                    context.Database.Migrate();
                    _configuracoesDb(context);
                }
            });
        }

        internal void AdicionarSetupBanco(Func<TContext, Task> setup)
            => _configuracoesDb = setup;

        private void GerarDatabaseConnectionString(ConfiguracoesTestes configuracoes)
        {
            _connectionStringMaster = configuracoes.ConnectionString;
            _connectionStringProcessada = _connectionStringMaster.Replace("master", _nomeInstanciaBanco);

            _nomeInstanciaBanco = $"Logs_{Guid.NewGuid().ToString().Replace("-", "")}";

            string sqlQuery = $"CREATE DATABASE {_nomeInstanciaBanco}";
            using var conexao = new SqlConnection(_connectionStringMaster);
                conexao.Execute(sqlQuery);            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            using var conexao = new SqlConnection(_connectionStringMaster);
            conexao.Execute(QueryDisposeDb());
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