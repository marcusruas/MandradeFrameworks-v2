using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Models
{
    public abstract class BaseSpecificationPaginated<T> : BaseSpecification<T>
    {
        public BaseSpecificationPaginated(int pagina, int quantidadeRegistros)
        {
            Pagina = pagina;
            QuantidadeRegistros = quantidadeRegistros;
        }

        public BaseSpecificationPaginated(Expression<Func<T, bool>> criteria, int pagina, int quantidadeRegistros) : base(criteria)
        {
            Pagina = pagina;
            QuantidadeRegistros = quantidadeRegistros;
        }

        /// <summary>
        /// Página a ser pesquisada
        /// </summary>
        public int Pagina { get; }
        /// <summary>
        /// Quantidade de registros da página
        /// </summary>
        public int QuantidadeRegistros { get; }
    }
}
