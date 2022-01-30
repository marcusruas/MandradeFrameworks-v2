using MandradeFrameworks.SharedKernel.Exceptions;
using MandradeFrameworks.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Usuario
{
    public class UsuarioAutenticado : IUsuarioAutenticado
    {
        public UsuarioAutenticado()
        {
            NomeCompleto = NOME_USUARIO_GENERICO;
            Funcional = FUNCIONAL_USUARIO_GENERICO;
            Permissoes = new List<string>();
        }

        public string NomeCompleto { get; private set; }
        public Funcional Funcional { get; private set; }
        public IEnumerable<string> Permissoes { get; private set; }

        private const string NOME_USUARIO_GENERICO = "Token Anônimo";
        private const string FUNCIONAL_USUARIO_GENERICO = "00011111111";

        public bool EstaAutenticado()
            => NomeCompleto != NOME_USUARIO_GENERICO && Funcional != FUNCIONAL_USUARIO_GENERICO;

        public bool PossuiPermissao(string permissao)
        {
            if (string.IsNullOrWhiteSpace(permissao))
                return true;

            return Permissoes.Any(pm => pm == permissao);
        }

        public void AlterarDadosUsuario(JwtSecurityToken token)
        {
            if (token is null)
                throw new TokenInvalidoException();

            string nome = ObterPropriedade(token, "nome");
            Funcional funcional = Convert.ToInt32(ObterPropriedade(token, "userId"));
            IEnumerable<string> permissoes = ObterPropriedade(token, "api_roles").Split(",");

            NomeCompleto = nome;
            Funcional = funcional;
            Permissoes = permissoes;
        }

        private string ObterPropriedade(JwtSecurityToken token, string propriedade)
            => token.Claims.FirstOrDefault(claim => claim.Type == propriedade).Value;
    }
}
