using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Models
{
    public abstract class BaseSpecificationPaginated<T> : BaseSpecification<T>
    {
        /// <summary>
        /// Página a ser pesquisada
        /// </summary>
        public int Pagina { get; private set; }
        /// <summary>
        /// Quantidade de registros da página
        /// </summary>
        public int QuantidadeRegistros { get; private set; }

        /// <summary>
        /// Adiciona paginação a query de specification
        /// </summary>
        /// <param name="pagina">Página a ser pesquisada</param>
        /// <param name="quantidadeRegistros">Quantidade de registros por página</param>
        public void AdicionarPaginacao(int pagina, int quantidadeRegistros)
        {
            Pagina = pagina;
            QuantidadeRegistros = quantidadeRegistros;
        }
    }
}
