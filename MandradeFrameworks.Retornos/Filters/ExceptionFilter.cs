using MandradeFrameworks.Mensagens;
using MandradeFrameworks.Mensagens.Exceptions;
using MandradeFrameworks.Retornos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MandradeFrameworks.Retornos.Filters
{
    internal class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            DefinirStatusCodeRetorno(context);
            GerarModeloRetorno(context);
        }

        private void DefinirStatusCodeRetorno(ExceptionContext context)
        {
            int statusCodeRetorno;

            if (context.Exception is ControlledException exception)
                statusCodeRetorno = exception.CodigoRetorno;
            else
                statusCodeRetorno = (int)HttpStatusCode.InternalServerError;

            context.HttpContext.Response.StatusCode = statusCodeRetorno;
        }

        private void GerarModeloRetorno(ExceptionContext context)
        {
            string mensagemErro;

            if (context.Exception is ControlledException exception)
                mensagemErro = exception.MensagemPadrao;
            else
                mensagemErro = "Não foi possível processar sua solicitação. Verifique os dados e tente novamente.";

            var mensageria = ObterServico<IMensageria>(context);
            var modelo =  new RetornoApi<string>(false, mensagemErro, mensageria.Mensagens);

            context.Result = new ObjectResult(modelo);
        }

        private T ObterServico<T>(ExceptionContext context)
            => (T)context.HttpContext.RequestServices.GetService(typeof(T));
    }
}
