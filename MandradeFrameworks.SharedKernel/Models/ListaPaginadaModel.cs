using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Models
{
    public class ListaPaginadaModel<T> where T : class
    {
        public ListaPaginadaModel() { }

        public IEnumerable<T> Itens { get; set; }
        /// <summary>
        /// Página anterior da pesquisa
        /// </summary>
        public int? PaginaAnterior { get; set; }
        /// <summary>
        /// Página atual da pesquisa
        /// </summary>
        public int PaginaAtual { get; set; }
        /// <summary>
        /// Próxima página da pesquisa
        /// </summary>
        public int? ProximaPagina { get; set; }
        /// <summary>
        /// Retorna se a página atual é a primeira página
        /// </summary>
        public bool PrimeiraPagina { get; set; }
        /// <summary>
        /// Retorna se a página atual é a última página
        /// </summary>
        public bool UltimaPagina { get; set; }
        /// <summary>
        /// Retorna a quantidade total de registros da pesquisa
        /// </summary>
        public int QuantidadeTotalRegistros { get; set; }
        /// <summary>
        /// Quantidade total de páginas da pesquisa
        /// </summary>
        public int QuantidadeTotalPaginas { get; set; }
        /// <summary>
        /// Quantidade de registros solicitados
        /// </summary>
        private int QuantidadeRegistrosSolicitada { get; set; }
    }
}
