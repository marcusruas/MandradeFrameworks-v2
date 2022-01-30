using MandradeFrameworks.SharedKernel.Exceptions;
using MandradeFrameworks.SharedKernel.Usuario;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace MandradeFrameworks.Autenticacao.Filters
{
    public class EndpointRestrito : ActionFilterAttribute
    {
        public EndpointRestrito() { }

        private JwtSecurityToken _token;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(token))
                throw new TokenInvalidoException();

            if (TokenValido(token))
                throw new TokenInvalidoException();

            GerarUsuarioCadastrado(context);
        }

        private bool TokenValido(string token)
        {
            var tokenFormatado = token.Replace("Bearer", string.Empty).Trim();
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(tokenFormatado))
                return false;

            _token = handler.ReadJwtToken(tokenFormatado);

            if (_token is null)
                return false;

            return true;
        }

        private void GerarUsuarioCadastrado(ActionExecutingContext context)
        {
            var usuario = context.HttpContext.RequestServices.GetService(typeof(IUsuarioAutenticado)) as IUsuarioAutenticado;
            usuario.AlterarDadosUsuario(_token);
        }
    }
}