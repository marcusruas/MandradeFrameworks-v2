using MandradeFrameworks.Mensagens;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MandradeFrameworks.Repositorios.Models;
using System.Linq;
using MandradeFrameworks.SharedKernel.Models;

namespace MandradeFrameworks.Repositorios.Persistence.Sql.ContextRepository
{
    public abstract class StandardSqlRepository<TContext> : StandardSqlRepository, IContextRepository 
    where TContext : StandardContext
    {
        public StandardSqlRepository(IServiceProvider provider, TContext context) : base(provider)
        {
            _context = context;
        }

        protected readonly TContext _context;

        public async Task<int> AdicionarEntidade<T>(T entidade)
        {
            try
            {
                await _context.AddAsync(entidade);
                int linhasAfetadas = await _context.SaveChangesAsync();

                if (linhasAfetadas < 0)
                    _mensageria.AdicionarMensagemErro("Não foi possível realizar esta operação no momento. Tente novamente mais tarde");

                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                _mensageria.AdicionarMensagemErro("Ocorreu um erro ao realizar esta operação.");
                _mensageria.AdicionarMensagemErro(ex.ToString());

                return -1;
            }
        }

        public async Task<int> AdicionarEntidade<T>(List<T> entidades)
        {
            try
            {
                await _context.AddRangeAsync(entidades);
                int linhasAfetadas = await _context.SaveChangesAsync();

                if (linhasAfetadas < 0)
                    _mensageria.AdicionarMensagemErro("Não foi possível realizar esta operação no momento. Tente novamente mais tarde");

                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                _mensageria.AdicionarMensagemErro("Ocorreu um erro ao realizar esta operação.");
                _mensageria.AdicionarMensagemErro($"Dados do erro: {ex}");

                return -1;
            }
        }

        public async Task<int> AlterarEntidade<T>(T entidade)
        {
            try
            {
                _context.Update(entidade);
                int linhasAfetadas = await _context.SaveChangesAsync();

                if (linhasAfetadas < 0)
                    _mensageria.AdicionarMensagemErro("Não foi possível realizar esta operação no momento. Tente novamente mais tarde");

                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                _mensageria.AdicionarMensagemErro("Ocorreu um erro ao realizar esta operação.");
                _mensageria.AdicionarMensagemErro(ex.ToString());

                return -1;
            }
        }

        public async Task<int> AlterarEntidade<T>(List<T> entidades)
        {
            try
            {
                _context.UpdateRange(entidades);
                int linhasAfetadas = await _context.SaveChangesAsync();

                if (linhasAfetadas < 0)
                    _mensageria.AdicionarMensagemErro("Não foi possível realizar esta operação no momento. Tente novamente mais tarde");

                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                _mensageria.AdicionarMensagemErro("Ocorreu um erro ao realizar esta operação.");
                _mensageria.AdicionarMensagemErro($"Dados do erro: {ex}");

                return -1;
            }
        }

        public async Task<int> DeletarEntidade<T>(T entidade)
        {
            try
            {
                _context.Remove(entidade);
                int linhasAfetadas = await _context.SaveChangesAsync();

                if (linhasAfetadas < 0)
                    _mensageria.AdicionarMensagemErro("Não foi possível realizar esta operação no momento. Tente novamente mais tarde");

                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                _mensageria.AdicionarMensagemErro("Ocorreu um erro ao realizar esta operação.");
                _mensageria.AdicionarMensagemErro(ex.ToString());

                return -1;
            }
        }

        public async Task<int> DeletarEntidade<T>(List<T> entidades)
        {
            try
            {
                _context.RemoveRange(entidades);
                int linhasAfetadas = await _context.SaveChangesAsync();

                if (linhasAfetadas < 0)
                    _mensageria.AdicionarMensagemErro("Não foi possível realizar esta operação no momento. Tente novamente mais tarde");

                return linhasAfetadas;
            }
            catch (Exception ex)
            {
                _mensageria.AdicionarMensagemErro("Ocorreu um erro ao realizar esta operação.");
                _mensageria.AdicionarMensagemErro($"Dados do erro: {ex}");

                return -1;
            }
        }

        public async Task<List<T>> ConsultaComSpecification<T>(BaseSpecification<T> specification) where T : class
        {
            var query = AdicionarSpecification<T>(specification);
            return await query.ToListAsync();
        }

        public async Task<ListaPaginada<T>> ConsultaComSpecification<T>(BaseSpecificationPaginated<T> specification) where T : class
        {
            if (specification.Pagina == 0 || specification.QuantidadeRegistros == 0)
                _mensageria.AdicionarMensagemErro("Consulta utilizando paginação não pode ter Página 0 ou Quantidade de Registros 0.");

            if (_mensageria.PossuiErros())
                return null;

            var query = AdicionarSpecification<T>(specification);
            var registrosJaObtidos = specification.Pagina * specification.QuantidadeRegistros;

            var itens = await query
                    .Skip(registrosJaObtidos)
                    .Take(specification.QuantidadeRegistros)
                    .ToListAsync();

            var quantidadeTotalRegistros = await query.CountAsync();
            var quantidadePaginas = (double) quantidadeTotalRegistros / specification.QuantidadeRegistros;
            var quantidadeTotalPaginas = (int) Math.Ceiling(quantidadePaginas);

            return new ListaPaginada<T>(itens, specification.Pagina, quantidadeTotalPaginas);
        }

        private IQueryable<T> AdicionarSpecification<T>(BaseSpecification<T> specification) where T : class
        {
            var query = _context.Set<T>().AsQueryable();

            if (specification.Criterios != null)
                query = query.Where(specification.Criterios);

            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);

            if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}