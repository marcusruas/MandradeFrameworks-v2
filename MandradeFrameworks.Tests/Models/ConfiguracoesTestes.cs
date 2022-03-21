using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Tests.Models
{
    internal class ConfiguracoesTestes
    {
        public ConfiguracoesTestes(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Connection string da base que será criada para os testes de integração
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
