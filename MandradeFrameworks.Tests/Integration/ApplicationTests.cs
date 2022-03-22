using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using MandradeFrameworks.SharedKernel.Extensions;
using MandradeFrameworks.Tests.Models;
using System;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;

namespace MandradeFrameworks.Tests.Integration
{
    public class ApplicationTests<TStartup, TContext> : WebApplicationFactory<TStartup>
    where TStartup : class
    where TContext : DbContext
    {
        private Func<TContext, Task> _configuracoesDb;

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return base.CreateWebHostBuilder()
                .UseEnvironment("Tests");
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TContext>));
                services.Remove(descriptor);

                services.AddDbContext<TContext>(options => options.UseInMemoryDatabase("Testes"));

                var provider = services.BuildServiceProvider();
                using var scope = provider.CreateScope();
                {
                    var context = scope.ServiceProvider.GetRequiredService<TContext>();
                    context.Database.Migrate();
                    _configuracoesDb(context);
                }
            });
        }

        internal void AdicionarSetupBanco(Func<TContext, Task> setup)
            => _configuracoesDb = setup;
    }
}