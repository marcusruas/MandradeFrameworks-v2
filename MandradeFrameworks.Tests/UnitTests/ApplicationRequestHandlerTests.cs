using MandradeFrameworks.Mensagens;
using MandradeFrameworks.Mensagens.Services;
using MandradeFrameworks.SharedKernel.Usuario;
using MandradeFrameworks.Tests.UnitTests.Mocks;

namespace MandradeFrameworks.Tests.UnitTests
{
    public class ApplicationRequestHandlerTests : UnitTestsClass
    {
        public ApplicationRequestHandlerTests()
        {
            _serviceProviderMock = new IServiceProviderMock();
            _mensageria = new Mensageria();
            _usuarioAutenticado = new UsuarioAutenticado();

            _serviceProviderMock.DefinirInjecaoServico<IMensageria>(_mensageria);
            _serviceProviderMock.DefinirInjecaoServico<IUsuarioAutenticado>(_usuarioAutenticado);
        }

        protected IServiceProviderMock _serviceProviderMock;
        protected IMensageria _mensageria;
        protected IUsuarioAutenticado _usuarioAutenticado;
    }
}