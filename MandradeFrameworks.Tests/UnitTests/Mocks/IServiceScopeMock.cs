using System;
using MandradeFrameworks.Tests.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MandradeFrameworks.Tests.UnitTests.Mocks
{
    public class IServiceScopeMock : MockBuilder<IServiceScope>
    {
        public void ObterServiceProvider(IServiceProvider serviceProvider)
            => _mock.Setup(x => x.ServiceProvider).Returns(serviceProvider);
    }
}