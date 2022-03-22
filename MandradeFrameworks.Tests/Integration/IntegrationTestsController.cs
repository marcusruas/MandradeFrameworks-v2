using MandradeFrameworks.Mensagens.Models;
using MandradeFrameworks.Retornos.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MandradeFrameworks.Tests.Integration
{
    public abstract class IntegrationTestsController<TStartup, TContext>
    : IClassFixture<ApplicationTests<TStartup, TContext>>
    where TStartup : class 
    where TContext : DbContext
    {
        protected IntegrationTestsController(
            ApplicationTests<TStartup, TContext> app,
            Func<TContext, Task> setupDb
        )
        {
            app.AdicionarSetupBanco(setupDb);

            Cliente = app.CreateClient();
            UrlBase = $"{app.Server.BaseAddress}{UrlControllerBase()}";
        }

        protected readonly HttpClient Cliente;
        protected readonly string UrlBase;

        /// <summary>
        /// Url obrigatória para definir o caminho completo da url base do client
        /// </summary>
        /// <returns></returns>
        protected abstract string UrlControllerBase();

        /// <summary>
        /// Gera um objeto <see cref="HttpContent"/> com o objeto passado como parâmetro convertido para body da requisição.
        /// </summary>
        protected HttpContent GerarCorpoRequisicao(object corpo, bool jsonPatch = false)
        {
            var json = JsonConvert.SerializeObject(corpo);
            string contentType = jsonPatch ? "application/json-patch+json" : "application/json";

            return new StringContent(json, Encoding.UTF8, contentType);
        }

        /// <summary>
        /// Converte a resposta obtida através da requisição a API para um modelo de retorno padrão da API
        /// </summary>
        protected async Task<RetornoApi<T>> DesserializarResposta<T>(HttpResponseMessage response)
        {
            try
            {
                var conteudoResposta = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RetornoApi<T>>(conteudoResposta);
            }
            catch
            {
                return new RetornoApi<T>(false, default, new List<Mensagem>());
            }
        }

        /// <summary>
        /// Converte a resposta obtida através da requisição a API para um modelo de retorno padrão da API
        /// </summary>
        protected async Task<RetornoApi<string>> DesserializarResposta(HttpResponseMessage response)
        {
            try
            {
                var conteudoResposta = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RetornoApi<string>>(conteudoResposta);
            }
            catch
            {
                return new RetornoApi<string>(false, default, new List<Mensagem>());
            }
        }
    }
}
