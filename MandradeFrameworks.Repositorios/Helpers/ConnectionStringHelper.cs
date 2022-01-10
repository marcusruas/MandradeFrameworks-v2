using MandradeFrameworks.Mensagens;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Repositorios.Helpers
{
    internal class ConnectionStringHelper
    {
        public ConnectionStringHelper(IConfiguration configuration, IMensageria mensageria)
        {
            _configuration = configuration;
            _mensageria = mensageria;
        }

        public ConnectionStringHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration;
        private IMensageria _mensageria;

        private const string SECCAO_CONNECTION_STRINGS = "ConnectionStrings";

        /// <summary>
        /// Obtém a connection string dentro do objeto "ConnectionStrings" dentro do JSON de configurações da API.
        /// 
        /// Caso uma entrada no objeto "ConnectionStrings" com o nome <see cref="nomeBanco"/> não seja encontrado,
        /// será adicionado uma mensagem do tipo <see cref="TipoMensagem.Erro"/> no objeto de mensageria e será retornado null.
        /// Caso não seja fornecido o objeto de mensageria no construtor dessa classe, será enviado apenas uma exception padrão com a mensagem de erro.
        /// </summary>
        /// <param name="nomeBanco">o nome do banco de dados localizado no objeto "ConnectionStrings"</param>
        /// <returns>O valor da entrada no objeto mencionado</returns>
        public string ObterConnectionString(string nomeBanco)
        {
            string mensagemErro = $"Não foi possível localizar a connection string do banco {nomeBanco}";

            string ConnectionStringLocation = $"{SECCAO_CONNECTION_STRINGS}:{nomeBanco}";
            var cnn = _configuration.GetSection(ConnectionStringLocation).Value;

            if (string.IsNullOrWhiteSpace(cnn))
            {
                if (_mensageria is null)
                    throw new Exception(mensagemErro);
                else
                    _mensageria.AdicionarMensagemErro(mensagemErro);
            }                

            return cnn;
        }
    }
}
