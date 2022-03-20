using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Models
{
    /// <summary>
    /// Objeto para retornar uma lista paginada.
    /// </summary>
    public class ListaPaginada<T> where T : class
    {
        public ListaPaginada(IEnumerable<T> itens, int paginaAtual, int quantidadeTotalRegistros)
        {
            Itens = itens;
            PaginaAtual = paginaAtual;
            QuantidadeTotalRegistros = quantidadeTotalRegistros;
        }

        public IEnumerable<T> Itens { get; }
        /// <summary>
        /// Página atual da pesquisa
        /// </summary>
        public int PaginaAtual { get; }
        /// <summary>
        /// Retorna a quantidade total de registros da pesquisa
        /// </summary>
        public int QuantidadeTotalRegistros { get; }
        /// <summary>
        /// Quantidade total de páginas da pesquisa
        /// </summary>
        public int QuantidadeTotalPaginas { get => ObterQuantidadeTotalPaginas(); }
        /// <summary>
        /// Retorna se a página atual é a primeira página
        /// </summary>
        public bool PrimeiraPagina { get => PaginaAtual == 1; }
        /// <summary>
        /// Retorna se a página atual é a última página
        /// </summary>
        public bool UltimaPagina { get => PaginaAtual == QuantidadeTotalPaginas;  }

        private int ObterQuantidadeTotalPaginas()
        {
            var quantidadeRegistrosAtual = Itens.Count();

            if (quantidadeRegistrosAtual == 0)
                return 1;

            var quantidadetotalPaginas = (double)QuantidadeTotalRegistros / quantidadeRegistrosAtual;
            return (int)Math.Ceiling(quantidadetotalPaginas);
        }
    }
}