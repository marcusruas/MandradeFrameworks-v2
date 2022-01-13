using MandradeFrameworks.Mensagens;
using MandradeFrameworks.Retornos.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MandradeFrameworks.Retornos.Controllers
{
    public abstract class StandardController : ControllerBase
    {
        public StandardController(IMediator mediador)
        {
            _mediador = mediador;
        }

        protected readonly IMediator _mediador;

        /// <summary>
        /// Passa o objeto do tipo <see cref="IRequest" /> para o mediador do tipo <see cref="IMediator"/> e retorna a resposta formatada pronta para o endpoint
        /// </summary>
        protected async Task<RetornoApi<T>> ProcessarSolicitacao<T>(IRequest<T> solicitacao)
        {
            var retorno = await _mediador.Send(solicitacao);
            return RespostaPadrao(retorno);
        }

        /// <summary>
        /// Converte o objeto passado para o formato padrão de retorno de endpoints
        /// </summary>
        protected RetornoApi<T> RespostaPadrao<T>(T dados)
        {
            var _mensageria = ObterServico<IMensageria>();
            Response.StatusCode = (int)ObterStatusCodeRetorno(_mensageria);

            return new RetornoApi<T>(dados, _mensageria.Mensagens);
        }

        private T ObterServico<T>()
            => (T)HttpContext.RequestServices.GetService(typeof(T));

        private HttpStatusCode ObterStatusCodeRetorno(IMensageria mensageria)
        {
            if (mensageria.PossuiErros())
                return HttpStatusCode.InternalServerError;

            if (mensageria.PossuiFalhasValidacao())
                return HttpStatusCode.BadRequest;

            return HttpStatusCode.OK;
        }
    }
}
