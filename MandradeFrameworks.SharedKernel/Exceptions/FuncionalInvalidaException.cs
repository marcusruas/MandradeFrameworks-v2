using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Exceptions
{
    public class FuncionalInvalidaException : ControlledException
    {
        public FuncionalInvalidaException()
        {
            MensagemPadrao = "Funcional inválida";
        }

        public override int CodigoRetorno => 400;
        public override string MensagemPadrao { get; }
    }
}
