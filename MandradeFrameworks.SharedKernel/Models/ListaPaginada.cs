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
        public ListaPaginada(IEnumerable<T> itens, int paginaAtual, int quantidadePaginas)
        {
            Itens = itens;
            PaginaAtual = paginaAtual;
            QuantidadePaginas = quantidadePaginas;
        }

        public IEnumerable<T> Itens { get; }
        /// <summary>
        /// Página atual da pesquisa
        /// </summary>
        public int PaginaAtual { get; }
        /// <summary>
        /// Quantidade total de páginas da pesquisa
        /// </summary>
        public int QuantidadePaginas { get; }
        /// <summary>
        /// Retorna a quantidade total de registros da pesquisa
        /// </summary>
        public int QuantidadeTotalRegistros { get => Itens.Count() * QuantidadePaginas; }
        /// <summary>
        /// Retorna se a página atual é a primeira página
        /// </summary>
        public bool PrimeiraPagina { get => PaginaAtual == 1; }
        /// <summary>
        /// Retorna se a página atual é a última página
        /// </summary>
        public bool UltimaPagina { get => PaginaAtual == QuantidadePaginas;  }
    }
}