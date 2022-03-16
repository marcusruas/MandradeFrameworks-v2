using MandradeFrameworks.Repositorios.Models;
using MandradeFrameworks.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MandradeFrameworks.Mensagens.Models;

namespace MandradeFrameworks.Repositorios.Persistence.Sql.ContextRepository
{
    public interface IContextRepository : IRepository
    {
        /// <summary>
        /// Adiciona uma entidade ao banco de dados. Caso o método <see cref="DbContext.SaveChangesAsync"/> retorne -1 ou ocorra alguma exception no processo,
        /// será adicionado uma mensagem do tipo <see cref="TipoMensagem.Erro"/> no sistema de mensageria e será retornado -1 no retorno desta operação
        /// </summary>
        /// <returns>A quantidade de registros afetados por essa operação</returns>
        Task<int> AdicionarEntidade<T>(T entidade);
        /// <summary>
        /// Adiciona mais de uma entidade ao banco de dados. Caso o método <see cref="DbContext.SaveChangesAsync"/> retorne -1 ou ocorra alguma exception no processo,
        /// será adicionado uma mensagem do tipo <see cref="TipoMensagem.Erro"/> no sistema de mensageria e será retornado -1 no retorno desta operação
        /// </summary>
        /// <returns>A quantidade de registros afetados por essa operação</returns>
        Task<int> AdicionarEntidade<T>(List<T> entidades);
        /// <summary>
        /// Altera uma entidade ao banco de dados. Caso o método <see cref="DbContext.SaveChangesAsync"/> retorne -1 ou ocorra alguma exception no processo,
        /// será adicionado uma mensagem do tipo <see cref="TipoMensagem.Erro"/> no sistema de mensageria e será retornado -1 no retorno desta operação
        /// </summary>
        /// <returns>A quantidade de registros afetados por essa operação</returns>
        Task<int> AlterarEntidade<T>(T entidade);
        /// <summary>
        /// Altera mais de uma entidade ao banco de dados. Caso o método <see cref="DbContext.AddAsync"/> retorne -1 ou ocorra alguma exception no processo,
        /// será adicionado uma mensagem do tipo <see cref="TipoMensagem.Erro"/> no sistema de mensageria e será retornado -1 no retorno desta operação
        /// </summary>
        /// <returns>A quantidade de registros afetados por essa operação</returns>
        Task<int> AlterarEntidade<T>(List<T> entidades);
        /// <summary>
        /// Deleta uma entidade ao banco de dados. Caso o método <see cref="DbContext.SaveChangesAsync"/> retorne -1 ou ocorra alguma exception no processo,
        /// será adicionado uma mensagem do tipo <see cref="TipoMensagem.Erro"/> no sistema de mensageria e será retornado -1 no retorno desta operação
        /// </summary>
        /// <returns>A quantidade de registros afetados por essa operação</returns>
        Task<int> DeletarEntidade<T>(T entidade);
        /// <summary>
        /// Deleta mais de uma entidade ao banco de dados. Caso o método <see cref="DbContext.AddAsync"/> retorne -1 ou ocorra alguma exception no processo,
        /// será adicionado uma mensagem do tipo <see cref="TipoMensagem.Erro"/> no sistema de mensageria e será retornado -1 no retorno desta operação
        /// </summary>
        /// <returns>A quantidade de registros afetados por essa operação</returns>
        Task<int> DeletarEntidade<T>(List<T> entidades);
        /// <summary>
        /// Realiza uma query em cima do context informado utilizando um membro herdado da classe <see cref="BaseSpecification{T}"/>
        /// </summary>
        /// <returns>Uma lista de entidades do tipo especificado que satisfaça os critérios informados na classe <see cref="BaseSpecification{T}"/></returns>
        Task<List<T>> ConsultaComSpecification<T>(BaseSpecification<T> specification) where T : class;
        /// <summary>
        /// Realiza uma query em cima do context informado utilizando um membro herdado da classe <see cref="BaseSpecification{T}"/> e retorna
        /// uma lista embrulhada no objeto <see cref="ListaPaginada{T}"/>. Caso ocorra algum erro, será adicionado uma mensagem 
        /// do tipo <see cref="TipoMensagem.Erro"/> no sistema de mensageria e será retornado null no retorno desta operação
        /// </summary>
        /// <returns>Uma lista de entidades do tipo especificado que satisfaça os critérios informados na classe <see cref="BaseSpecification{T}"/></returns>
        Task<ListaPaginada<T>> ConsultaComSpecification<T>(BaseSpecificationPaginated<T> specification) where T : class;
    }
}
