using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Mensagens.Exceptions
{
    public class FalhaValidacaoException : ControlledException
    {
        public FalhaValidacaoException()
        {
            MensagemPadrao = "Uma ou mais informações solicitadas são inválidas. Contate o Suporte para mais informações.";
        }

        public override int CodigoRetorno => 400;
        public override string MensagemPadrao { get; }
    }
}
