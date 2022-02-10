using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Tests.Models
{
    public class ConfiguracoesTestes
    {
        public ConfiguracoesTestes() {  }

        /// <summary>
        /// Connection string da base que será criada para os testes de integração
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Token que será inserido por padrão no client
        /// </summary>
        public string TokenPadraoTestes { get; set; }
    }
}
