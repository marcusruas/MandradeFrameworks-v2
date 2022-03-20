using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Exceptions
{
    public class PaginacaoInvalidaException : ControlledException
    {
        public PaginacaoInvalidaException()
        {
            MensagemPadrao = "Quantidade de registros selecionada é inválida. Quantidade deve ser maior que 0";
        }
        
        public PaginacaoInvalidaException(int primeiraPagina, int ultimaPagina)
        {
            MensagemPadrao = $"Página selecionada é inválida. Para esta quantidade de registros, a página selecionada deve estar entre {primeiraPagina} e {ultimaPagina}.";
        }

        public override int CodigoRetorno => 400;
        public override string MensagemPadrao { get; }
    }
}
