using MandradeFrameworks.Tests.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Tests.Configuration
{
    public static class TestsConfiguration
    {
        public static IServiceCollection AdicionarTestes(this IServiceCollection servicos, ConfiguracoesTestes configuracoes)
        {
            servicos.AddSingleton(configuracoes);
            return servicos;
        }
    }
}
