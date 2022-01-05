using MandradeFrameworks.Mensagens.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Mensagens.Configuration
{
    public static class MensageriaConfiguration
    {
        public static IServiceCollection AdicionarMensageria(this IServiceCollection servicos)
        {
            servicos.AddScoped<IMensageria, Mensageria>();
            return servicos;
        }
    }
}
