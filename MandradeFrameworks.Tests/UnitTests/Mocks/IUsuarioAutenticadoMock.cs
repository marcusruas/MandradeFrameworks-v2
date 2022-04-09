using System.Collections.Generic;
using MandradeFrameworks.SharedKernel.Usuario;
using MandradeFrameworks.Tests.Models;

namespace MandradeFrameworks.Tests.UnitTests.Mocks
{
    public class IUsuarioAutenticadoMock : MockBuilder<IUsuarioAutenticado>
    {
        public void DefinirUsuario(string usuario)
            => _mock.Setup(x => x.NomeCompleto).Returns(usuario);

        public void DefinirPermissoes(List<string> permissoes)
            => _mock.Setup(x => x.Permissoes).Returns(permissoes);

        public void DefinirEstaAutenticado(bool estaAutenticado)
            => _mock.Setup(x => x.EstaAutenticado()).Returns(estaAutenticado);

        public void DefinirPermissoes(string permissao, bool possuiPermissao)
            => _mock.Setup(x => x.PossuiPermissao(permissao)).Returns(possuiPermissao);
    }
}