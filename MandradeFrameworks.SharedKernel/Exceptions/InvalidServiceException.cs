using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Exceptions
{
    public class ServicoInvalidoException : ControlledException
    {
        public ServicoInvalidoException(string servicoInvalido)
        {
            MensagemPadrao = $"Não foi possível localizar um serviço registrado do tipo {servicoInvalido}";
        }

        public override int CodigoRetorno => 500;
        public override string MensagemPadrao { get; }
    }
}
