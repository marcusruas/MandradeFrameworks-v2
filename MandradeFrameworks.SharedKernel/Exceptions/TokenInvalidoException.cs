using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Exceptions
{
    public class TokenInvalidoException : ControlledException
    {
        public TokenInvalidoException()
        {
            MensagemPadrao = "Token informado para a solicitação é inválido.";
        }

        public override int CodigoRetorno => 400;
        public override string MensagemPadrao { get; }
    }
}
