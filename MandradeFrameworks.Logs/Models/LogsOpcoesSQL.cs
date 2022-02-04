using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Logs.Models
{
    public class LogsOpcoesSQL
    {
        public LogsOpcoesSQL(string connectionString, string tabela, string schema = null, bool logsHabilitados = true)
        {
            ConnectionString = connectionString;
            Schema = schema;
            Tabela = tabela;
            LogsHabilitados = logsHabilitados;
        }

        /// <summary>
        /// Connection String do banco SQL Server onde será armazenado a tabela de logs.
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Schema da tabela que irá gravar os logs da aplicação. caso não haja schema, pode ser passado null.
        /// </summary>
        public string Schema { get; set; }
        /// <summary>
        /// Nome da tabela que irá gravar os logs da aplicação.
        /// </summary>
        public string Tabela { get; }
        /// <summary>
        /// Informa se a aplicação está habilitada para logs.
        /// </summary>
        public bool LogsHabilitados { get; }
    }
}
