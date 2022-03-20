using MandradeFrameworks.SharedKernel.Exceptions;
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
        public ListaPaginada(IEnumerable<T> itens, int paginaAtual, int quantidadeTotalRegistros, int quantidadeRegistrosSolicitada)
        {
            Itens = itens;
            PaginaAtual = paginaAtual;
            QuantidadeTotalRegistros = quantidadeTotalRegistros;
            QuantidadeRegistrosSolicitada = quantidadeRegistrosSolicitada;

            ValidarPaginacao();
        }

        public IEnumerable<T> Itens { get; }
        /// <summary>
        /// Página anterior da pesquisa
        /// </summary>
        public int? PaginaAnterior { get => ObterPaginaAnterior(); }
        /// <summary>
        /// Página atual da pesquisa
        /// </summary>
        public int PaginaAtual { get; }
        /// <summary>
        /// Próxima página da pesquisa
        /// </summary>
        public int? ProximaPagina { get => ObterProximaPagina(); }
        /// <summary>
        /// Retorna se a página atual é a primeira página
        /// </summary>
        public bool PrimeiraPagina { get => PaginaAtual == 1; }
        /// <summary>
        /// Retorna se a página atual é a última página
        /// </summary>
        public bool UltimaPagina { get => PaginaAtual == QuantidadeTotalPaginas; }
        /// <summary>
        /// Retorna a quantidade total de registros da pesquisa
        /// </summary>
        public int QuantidadeTotalRegistros { get; }
        /// <summary>
        /// Quantidade total de páginas da pesquisa
        /// </summary>
        public int QuantidadeTotalPaginas { get => ObterQuantidadeTotalPaginas(); }
        /// <summary>
        /// Quantidade de registros solicitados
        /// </summary>
        private int QuantidadeRegistrosSolicitada { get; }

        private int ObterQuantidadeTotalPaginas()
        {
            var quantidadetotalPaginas = (double)QuantidadeTotalRegistros / QuantidadeRegistrosSolicitada;
            return (int) Math.Ceiling(quantidadetotalPaginas);
        }

        private int? ObterPaginaAnterior()
        {
            if (PrimeiraPagina)
                return null;

            return PaginaAtual - 1;
        }

        private int? ObterProximaPagina()
        {
            if (UltimaPagina)
                return null;

            return PaginaAtual + 1;
        }

        private void ValidarPaginacao()
        {
            int UltimaPagina = ObterQuantidadeTotalPaginas();

            if (QuantidadeRegistrosSolicitada <= 0)
                throw new PaginacaoInvalidaException();

            if (PaginaAtual < 1 || PaginaAtual > UltimaPagina)
                throw new PaginacaoInvalidaException(1, UltimaPagina);
        }
    }
}