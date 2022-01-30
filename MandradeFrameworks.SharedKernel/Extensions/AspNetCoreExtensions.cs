using MandradeFrameworks.SharedKernel.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Extensions
{
    public static class AspNetCoreExtensions
    {
        /// <summary>
        /// Obtém serviço registrado do <see cref="IServiceProvider"/>. Caso o mesmo serviço não tenha sido registrado, será jogado uma exception
        /// do tipo <see cref="ServicoInvalidoException"/>
        /// </summary>
        /// <typeparam name="TService">Tipo do serviço registrado</typeparam>
        public static TService ObterServico<TService>(this IServiceProvider services)
        {
            var tipoServico = typeof(TService);
            var service = (TService)services.GetService(tipoServico);

            if (service is null)
                throw new ServicoInvalidoException(tipoServico.ToString());

            return service;
        }

        /// <summary>
        /// Obtém serviço registrado do <see cref="HttpContext"/>. Caso o mesmo serviço não tenha sido registrado, será jogado uma exception
        /// do tipo <see cref="ServicoInvalidoException"/>
        /// </summary>
        /// <typeparam name="TService">Tipo do serviço registrado</typeparam>
        public static TService ObterServico<TService>(this HttpContext context)
        {
            var tipoServico = typeof(TService);
            var service = (TService)context.RequestServices.GetService(tipoServico);

            if (service is null)
                throw new ServicoInvalidoException(tipoServico.ToString());

            return service;
        }
    }
}
