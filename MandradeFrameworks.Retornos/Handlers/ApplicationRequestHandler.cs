using MandradeFrameworks.Mensagens;
using MandradeFrameworks.SharedKernel.Extensions;
using MandradeFrameworks.SharedKernel.Usuario;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MandradeFrameworks.Retornos.Handlers
{
    public abstract class ApplicationRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Instancia de um request handler com o seu <see cref="HttpContext"/>
        /// </summary>
        /// <param name="context">context para que seja possível obter os serviços necessários da aplicação</param>
        public ApplicationRequestHandler(IServiceProvider services)
        {
            _mensageria = services.ObterServico<IMensageria>();
            _usuarioAutenticado = services.ObterServico<IUsuarioAutenticado>();
        }

        protected readonly IMensageria _mensageria;
        protected readonly IUsuarioAutenticado _usuarioAutenticado;

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
