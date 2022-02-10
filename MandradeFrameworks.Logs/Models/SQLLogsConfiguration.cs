﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Logs.Models
{
    public class SQLLogsConfiguration
    {
        public SQLLogsConfiguration() { }

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
        public string Tabela { get; set; }
    }
}
