using MandradeFrameworks.Logs.Models;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using Dapper;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;

namespace MandradeFrameworks.Logs.Configuration
{
    public static class LogsConfiguration
    {
        /// <summary>
        /// Método para adicionar logs na aplicação.
        /// </summary>
        public static void AdicionarLogs(SQLLogsConfiguration opcoes)
        {
            if (string.IsNullOrWhiteSpace(opcoes.ConnectionString))
                throw new ArgumentException("Connection String da base de logs não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(opcoes.Tabela))
                throw new ArgumentException("Nome da tabela de logs não pode ser vazio.");

            GerarTabelaSQL(opcoes);
            CriarInstanciaSerilog(opcoes);
        }

        private static void CriarInstanciaSerilog(SQLLogsConfiguration opcoes)
        {
            var additionalColumns = new Collection<SqlColumn> { new SqlColumn { ColumnName = "Action" } };
            var options = new ColumnOptions() { AdditionalColumns = additionalColumns };
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .MSSqlServer(
                    connectionString: opcoes.ConnectionString,
                    sinkOptions: new MSSqlServerSinkOptions { SchemaName = opcoes.Schema, TableName = opcoes.Tabela },
                    columnOptions: options
                )
                .CreateLogger();
        }

        private static void GerarTabelaSQL(SQLLogsConfiguration opcoes)
        {
            var consulta = QueryCriacaoTabela(opcoes);

            try
            {
                using var connection = new SqlConnection(opcoes.ConnectionString);
                connection.Execute(consulta);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu uma falha ao realizar o cadastro da tabela SQL. Detalhes: {ex}");
            }
        }

        private static string QueryCriacaoTabela(SQLLogsConfiguration opcoes)
        {
            string schema = string.IsNullOrWhiteSpace(opcoes.Schema) ? "dbo" : opcoes.Schema;
            string tabela = $"[{schema}].[{opcoes.Tabela}]";

            return $@"
                IF NOT EXISTS(OBJECT_ID('{tabela}'))
                BEGIN
                    CREATE TABLE {tabela} (
                       [Id] int IDENTITY(1,1) NOT NULL,
                       [Message] nvarchar(500) NULL,
                       [MessageTemplate] nvarchar(500) NULL,
                       [Level] nvarchar(128) NULL,
                       [TimeStamp] datetime NOT NULL,
                       [Exception] nvarchar(max) NULL,
                       [Properties] nvarchar(max) NULL,
                       [Action] nvarchar(max) NULL

                       CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC) 
                    );

                    CREATE INDEX IX_Consulta_Simplificada ON {tabela} (Data, Message)
                    CREATE INDEX IX_Consulta_Nivel ON {tabela} (Data, Level)
                END
            ";
        }
    }
}
