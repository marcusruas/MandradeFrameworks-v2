using MandradeFrameworks.Tests.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MandradeFrameworks.Tests.UnitTests.Mocks
{
    public class IServiceProviderMock : MockBuilder<IServiceProvider>
    {
        public void DefinirInjecaoServico<T>(T retorno)
            => _mock.Setup(x => x.GetService(typeof(T))).Returns(retorno);

        public void DefinirCriacaoEscopoParaSelf()
        {
            var serviceScopeMock = new IServiceScopeMock();
            serviceScopeMock.ObterServiceProvider(Build());

            _mock.Setup(x => x.CreateScope()).Returns(serviceScopeMock.Build());
        }
    }
}
