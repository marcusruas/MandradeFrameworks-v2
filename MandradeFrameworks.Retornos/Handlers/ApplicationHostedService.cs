using MandradeFrameworks.Retornos.Models;
using MandradeFrameworks.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MandradeFrameworks.Retornos.Handlers
{
    public abstract class ApplicationHostedService<TRequest> : IHostedService, IDisposable
    where TRequest : IRequest
    {
        /// <summary>
        /// Gera um novo hosted service completamente configurado.
        /// </summary>
        public ApplicationHostedService(IServiceProvider container, IConfiguration configuration)
        {
            NomeServico = GetType().Name;

            Container = container;
            Configuration = configuration;
        }

        protected bool ServicoAtivo { get; private set; }
        private readonly string NomeServico;

        private readonly IServiceProvider Container;
        private readonly IConfiguration Configuration;

        private Timer Timer;

        private void ExecutarRequest(object state)
        {
            using var escopo = Container.CreateScope();
            try
            {
                var mediador = escopo.ServiceProvider.ObterServico<IMediator>();
                var request = Activator.CreateInstance<TRequest>();

                mediador.Send(request).Wait();
            }
            catch (Exception e)
            {
                Log.Error(e, $"Falha ao executar o serviço {NomeServico}");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            string chaveServico = $"Jobs:{NomeServico}";
            var configuracoesServico = Configuration.GetValue<HostedServiceConfigs>(chaveServico);

            if (!configuracoesServico.ServicoHabilitado)
                return Task.CompletedTask;

            int intervaloSegundos = configuracoesServico.IntervaloSegundos == 0 ? 3600 : configuracoesServico.IntervaloSegundos;
            TimeSpan intervaloExecucao = TimeSpan.FromSeconds(intervaloSegundos);

            Timer = new Timer(ExecutarRequest, null, TimeSpan.Zero, intervaloExecucao);
            ServicoAtivo = true;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Timer?.Change(Timeout.Infinite, 0);
            ServicoAtivo = false;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Timer?.Dispose();
            ServicoAtivo = false;
        }
    }
}