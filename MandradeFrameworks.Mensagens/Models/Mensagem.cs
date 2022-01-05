using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Mensagens.Models
{
    public class Mensagem
    {
        public Mensagem(string valor)
        {
            Tipo = TipoMensagem.Informativa;
            Valor = valor;
        }

        public Mensagem(TipoMensagem tipo, string valor)
        {
            Tipo = tipo;
            Valor = valor;
        }

        public TipoMensagem Tipo { get; }
        public string Valor { get; }
    }
}
