using MandradeFrameworks.Logs.Models;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using Dapper;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using Serilog.Events;
using Microsoft.Extensions.Configuration;

namespace MandradeFrameworks.Logs.Configuration
{
    public static class LogsConfiguration
    {
        /// <summary>
        /// Método para adicionar logs na aplicação.
        /// </summary>
        /// <param name="configuration">Objeto de configurações da aplicação</param>
        /// <param name="tabela">Nome da tabela para gravação de logs.</param>
        /// <param name="schema">Schema da Tabela para gravação de logs. Caso não preenchido será 'dbo'</param>
        /// <param name="criarTabelaSeNaoExistir">Define se caso a tabela solicitada para gravação de logs não exista, ela será criada no DB da ConnectionString</param>
        public static void AdicionarLogs(IConfiguration configuration, string tabela, string schema = "dbo", bool criarTabelaSeNaoExistir = true)
        {
            string chaveCnnLogs = "Logs";
            string connectionStringLogs = configuration.GetConnectionString(chaveCnnLogs);

            var configsLogs = new SQLLogsConfiguration(connectionStringLogs, schema, tabela);

            if (string.IsNullOrWhiteSpace(configsLogs.ConnectionString))
                throw new ArgumentException($"Connection String da base de logs não pode ser vazio. A ConnectionString deve estar no objeto do AppSettings, chave '${chaveCnnLogs}'");

            if (string.IsNullOrWhiteSpace(configsLogs.Tabela))
                throw new ArgumentException("Nome da tabela de logs não pode ser vazio.");

            GerarTabelaSQL(configsLogs);
            CriarInstanciaSerilog(configsLogs);
        }

        private static void GerarTabelaSQL(SQLLogsConfiguration configs)
        {
            var consulta = QueryCriacaoTabela(configs);

            try
            {
                using var connection = new SqlConnection(configs.ConnectionString);
                connection.Execute(consulta);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu uma falha ao realizar o cadastro da tabela SQL. Detalhes: {ex}");
            }
        }

        private static string QueryCriacaoTabela(SQLLogsConfiguration configs)
        {
            string tabelaComSchema = $"[{configs.Schema}].[{configs.Tabela}]";

            return $@"
                IF (SELECT OBJECT_ID('{tabelaComSchema}')) IS NULL 
                BEGIN
                    CREATE TABLE {tabelaComSchema} (
                       [Id] int IDENTITY(1,1) NOT NULL,
                       [Message] nvarchar(500) NULL,
                       [MessageTemplate] nvarchar(500) NULL,
                       [Level] nvarchar(128) NULL,
                       [TimeStamp] datetime NOT NULL,
                       [Exception] nvarchar(max) NULL,
                       [Properties] nvarchar(max) NULL

                       CONSTRAINT [PK_{configs.Tabela}] PRIMARY KEY CLUSTERED ([Id] ASC) 
                    );

                    CREATE INDEX IX_Consulta_Simplificada ON {tabelaComSchema} (TimeStamp, Message)
                    CREATE INDEX IX_Consulta_Nivel ON {tabelaComSchema} (TimeStamp, Level)
                END
            ";
        }

        private static void CriarInstanciaSerilog(SQLLogsConfiguration configs)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
                .WriteTo.MSSqlServer(
                    connectionString: configs.ConnectionString,
                    sinkOptions: new MSSqlServerSinkOptions() 
                    { 
                        TableName = configs.Tabela, 
                        SchemaName = configs.Schema 
                    }
                )
                .CreateLogger();
        }
    }
}
