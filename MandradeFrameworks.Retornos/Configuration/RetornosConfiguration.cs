using MandradeFrameworks.Retornos.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Retornos.Configuration
{
    public static class RetornosConfiguration
    {
        public static MvcOptions AdicionarConfiguracoes(this MvcOptions configuracoes)
        {
            configuracoes.Filters.Add<ExceptionFilter>();
            return configuracoes;
        }
    }
}
