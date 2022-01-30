using MandradeFrameworks.SharedKernel.Usuario;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Autenticacao.Configuration
{
    public static class AutenticacaoConfiguration
    {
        public static IServiceCollection AdicionarAutenticacao(this IServiceCollection servicos)
        {
            servicos.AddScoped<IUsuarioAutenticado, UsuarioAutenticado>();
            return servicos;
        }
    }
}