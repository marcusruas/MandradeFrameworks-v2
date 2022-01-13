using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Mensagens.Exceptions
{
    public class ErroInternoException : ControlledException
    {
        public ErroInternoException()
        {
            MensagemPadrao = "Ocorreu uma falha ao enviar as informações. Reinicie o navegador ou tente novamente mais tarde";
        }

        public override int CodigoRetorno => 500;
        public override string MensagemPadrao { get; }
    }
}
