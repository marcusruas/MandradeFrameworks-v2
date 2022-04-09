using MandradeFrameworks.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Usuario
{
    public interface IUsuarioAutenticado
    {
        /// <summary>
        /// Nome completo do usuário autenticado
        /// </summary>
        string NomeCompleto { get; }
        /// <summary>
        /// Permissões que o usuário possui
        /// </summary>
        IEnumerable<string> Permissoes { get; }
        /// <summary>
        /// Valida se o usuário está autenticado
        /// </summary>
        bool EstaAutenticado();
        /// <summary>
        /// Valida se o usuário possui a permissão informada
        /// </summary>
        bool PossuiPermissao(string permissao);
        /// <summary>
        /// Altera os dados do usuário com base em um token de autenticação
        /// </summary>
        /// <param name="token">Token contendo as novas informações do usuário</param>
        void AlterarDadosUsuario(JwtSecurityToken token);
    }
}
