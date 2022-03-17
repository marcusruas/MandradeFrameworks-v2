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

        private readonly IConfiguration _configuration;
        private readonly IMensageria _mensageria;

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
            var cnn = _configuration.GetConnectionString(nomeBanco);

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
