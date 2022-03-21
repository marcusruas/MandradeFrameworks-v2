using MandradeFrameworks.Tests.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Tests.Configuration
{
    public static class TestsConfiguration
    {
        public static IServiceCollection AdicionarTestes(this IServiceCollection servicos, string connectionString)
        {
            var configuracoes = new ConfiguracoesTestes(connectionString);

            servicos.AddSingleton(configuracoes);
            return servicos;
        }
    }
}
