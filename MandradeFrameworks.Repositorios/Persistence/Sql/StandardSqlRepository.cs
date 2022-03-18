using MandradeFrameworks.Mensagens;
using MandradeFrameworks.Mensagens.Models;
using MandradeFrameworks.Repositorios.Helpers;
using MandradeFrameworks.Repositorios.Models;
using MandradeFrameworks.SharedKernel.Extensions;
using MandradeFrameworks.SharedKernel.Usuario;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandradeFrameworks.Repositorios.Persistence.Sql
{
    public abstract class StandardSqlRepository : IRepository
    {
        public StandardSqlRepository(IServiceProvider services)
        {
            _mensageria = services.ObterServico<IMensageria>();
            _configuration = services.ObterServico<IConfiguration>();
            _usuarioAutenticado = services.ObterServico<IUsuarioAutenticado>();

            PASTA_PADRAO_PROJETO = Path.GetDirectoryName(GetType().Assembly.CodeBase);
            DefinirSqlPath();
        }

        protected readonly IMensageria _mensageria;
        protected readonly IUsuarioAutenticado _usuarioAutenticado;

        private readonly IConfiguration _configuration;
        private string _sqlFolderPath;

        private const string CAMADA_PADRAO_REPOSITORIOS = "Infrastructure";
        private const string PASTA_PADRAO_REPOSITORIOS = "SQL";
        private readonly string PASTA_PADRAO_PROJETO;

        /// <summary>
        /// Obtém o conteudo (query SQL) do arquivo cujo nome é informado 
        /// 
        /// Caso o arquivo não possa ser encontrado, será adicionado uma mensagem do tipo <see cref="TipoMensagem.Erro"/> no objeto de mensageria e será retornado null.
        /// Caso haja qualquer tipo de erro ao tentar ler o arquivo, será adicionado uma mensagem de erro como no exemplo acima informando o que ocorreu.
        /// </summary>
        /// <param name="nomeArquivo">Nome do arquivo SQL que deseja ser obtido (não é necessário o .sql no final)</param>
        protected string ObterConteudoArquivoSql(string nomeArquivo)
        {
            try
            {
                string nomeCorrigidoArquivo = nomeArquivo.EndsWith(".sql") ? nomeArquivo.Replace(".sql", string.Empty) : nomeArquivo;
                string[] linhas;
                string conteudoArquivo = string.Empty;

                string pathArquivo = Path.Combine(PASTA_PADRAO_PROJETO, _sqlFolderPath, $"{nomeCorrigidoArquivo}.sql");
                pathArquivo = pathArquivo.Replace("file:\\", "");

                if (!File.Exists(pathArquivo))
                {
                    _mensageria.AdicionarMensagemErro($"Não foi possível localizar o arquivo de Consulta SQL '{nomeCorrigidoArquivo}'");
                    return null;
                }

                linhas = File.ReadAllLines(pathArquivo);

                foreach (var linha in linhas)
                    conteudoArquivo += $"{linha} ";

                return conteudoArquivo;
            }
            catch (ArgumentNullException) 
            {
                _mensageria.AdicionarMensagemErro("O arquivo de consulta ao banco de dados está vazio.");
                return null;
            } 
            catch (Exception ex) 
            {
                _mensageria.AdicionarMensagemErro(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Executa uma função feita pelo usuário com o intuito de obter dados do banco de dados cuja <see cref="SqlConnection"/> aponta.
        /// 
        /// Caso ocorra algum erro (exception) na função, será adicionado uma mensagem do tipo <see cref="TipoMensagem.Erro"/> no objeto 
        /// de mensageria e nenhuma outra ação será realizada.
        /// </summary>
        /// <param name="funcao">a função que será executada para passar comandos para o banco de dados</param>
        /// <returns>Uma task com o resultado da função.</returns>
        protected async Task ExecutarComandoSql(Func<SqlConnection, Task> funcao, string nomeBanco)
        {
            var conexao = ObterConexaoSql(nomeBanco);

            if (_mensageria.PossuiErros())
                return;

            try
            {
                conexao.Open();
                await funcao(conexao);
            }
            catch (Exception ex)
            {
                _mensageria.AdicionarMensagemErro($"Ocorreu um erro ao executar sua query SQL. Detalhes: {ex}");
            }
            finally
            {
                conexao.Close();
            }

        }

        private SqlConnection ObterConexaoSql(string nomeBanco)
        {
            var cnnHelper = new ConnectionStringHelper(_configuration, _mensageria);
            var connectionString = cnnHelper.ObterConnectionString(nomeBanco);

            if (_mensageria.PossuiErros())
                return null;

            return new SqlConnection(connectionString);
        }

        private void DefinirSqlPath()
        {
            var namespaces = GetType().Namespace.Split(".").ToList();
            namespaces = namespaces.Where(ns => ns != CAMADA_PADRAO_REPOSITORIOS).ToList();

            _sqlFolderPath = Path.Combine(
                PASTA_PADRAO_PROJETO, 
                string.Join("\\", namespaces),
                PASTA_PADRAO_REPOSITORIOS
            );
            _sqlFolderPath = _sqlFolderPath.Replace("file:\\", "");
        }
    }
}
