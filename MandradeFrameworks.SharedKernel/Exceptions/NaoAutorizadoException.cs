using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Exceptions
{
    public class NaoAutorizadoException : ControlledException
    {
        public NaoAutorizadoException(IEnumerable<string> permissoesObrigatorias)
        {
            string permissoesConcatenadas;

            if (permissoesObrigatorias is null || permissoesObrigatorias.Any())
                permissoesConcatenadas = "Permissão não identificada";
            else
                permissoesConcatenadas = string.Join(", ", permissoesObrigatorias);

            MensagemPadrao = $"Usuário não possui permissão de acesso para realizar a operação. As permissões obrigatórias para acesso são: {permissoesConcatenadas}";
        }

        public override int CodigoRetorno => 401;
        public override string MensagemPadrao { get; }
    }
}
