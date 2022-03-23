using MandradeFrameworks.SharedKernel.Exceptions;
using MandradeFrameworks.SharedKernel.Extensions;
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
        /// <summary>
        /// Define que para acessar o endpoint, deve-se passar no Header um Json Web Token (JWT) mas não há necessidade do usuário autenticado
        /// possuir uma permissão específica
        /// </summary>
        public EndpointRestrito()
        {
            _permissoesObrigatorias = new List<string>();
        }

        /// <summary>
        /// Define que para acessar o endpoint, deve-se passar no Header um Json Web Token (JWT) e possuir uma permissão especifica
        /// </summary>
        /// <param name="permissao">Permissão obrigatória para o acesso desse endpoint</param>
        public EndpointRestrito(string permissao)
        {
            _permissoesObrigatorias = new List<string>() { permissao };
        }

        /// <summary>
        /// Define que para acessar o endpoint, deve-se passar no Header um Json Web Token (JWT) e possuir uma permissão especifica
        /// </summary>
        /// <param name="permissoes">Permissões obrigatórias para o acesso desse endpoint</param>
        public EndpointRestrito(params string[] permissoes)
        {
            _permissoesObrigatorias = new List<string>(permissoes);
        }

        private IEnumerable<string> _permissoesObrigatorias;
        private JwtSecurityToken _token;
        private IUsuarioAutenticado _usuarioAutenticado;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(token))
                throw new TokenInvalidoException();

            if (!TokenValido(token))
                throw new TokenInvalidoException();

            GerarUsuarioCadastrado(context);
            ValidarPermissoesObrigatorias();
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
            _usuarioAutenticado = context.HttpContext.ObterServico<IUsuarioAutenticado>();
            _usuarioAutenticado.AlterarDadosUsuario(_token);
        }

        private void ValidarPermissoesObrigatorias()
        {
            if (!_permissoesObrigatorias.Any())
                return;

            if (_usuarioAutenticado.Permissoes is null || !_usuarioAutenticado.Permissoes.Any())
                throw new NaoAutorizadoException(_permissoesObrigatorias);

            bool naoPossuiPermissoes = _permissoesObrigatorias
                .Any(permissao => !_usuarioAutenticado.Permissoes.Any(p => p == permissao));

            if (naoPossuiPermissoes)
                throw new NaoAutorizadoException(_permissoesObrigatorias);
        }
    }
}