using MandradeFrameworks.Mensagens;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MandradeFrameworks.Retornos.Handlers
{
    public abstract class ApplicationRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public ApplicationRequestHandler(IMensageria mensageria)
        {
            _mensageria = mensageria;
        }

        protected readonly IMensageria _mensageria;

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
