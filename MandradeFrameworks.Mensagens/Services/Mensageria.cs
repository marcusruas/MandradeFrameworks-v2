﻿using MandradeFrameworks.Mensagens.Models;
using MandradeFrameworks.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MandradeFrameworks.Mensagens.Services
{
    internal class Mensageria : IMensageria
    {
        public Mensageria()
        {
            _mensagens = new List<Mensagem>();
        }

        public IEnumerable<Mensagem> Mensagens { get => _mensagens; }
        private ICollection<Mensagem> _mensagens { get; }

        public void AdicionarMensagemInformativa(string mensagem)
            => _mensagens.Add(new Mensagem(TipoMensagem.Informativa, mensagem));

        public bool PossuiInformativas()
            => _mensagens.Any(x => x.Tipo == TipoMensagem.Informativa);

        public void AdicionarMensagemAlerta(string mensagem)
            => _mensagens.Add(new Mensagem(TipoMensagem.Alerta, mensagem));

        public bool PossuiAlertas()
            => _mensagens.Any(x => x.Tipo == TipoMensagem.Alerta);

        public void AdicionarMensagemFalhaValidacao(string mensagem)
            => _mensagens.Add(new Mensagem(TipoMensagem.FalhaValidacao, mensagem));

        public bool PossuiFalhasValidacao()
            => _mensagens.Any(x => x.Tipo == TipoMensagem.FalhaValidacao);

        public void AdicionarMensagemErro(string mensagem)
            => _mensagens.Add(new Mensagem(TipoMensagem.Erro, mensagem));

        public bool PossuiErros()
            => _mensagens.Any(x => x.Tipo == TipoMensagem.Erro);

        public void RetornarMensagemErro(string mensagem)
        {
            AdicionarMensagemErro(mensagem);
            throw new ErroInternoException();
        }

        public void RetornarMensagemFalhaValidacao(string mensagem)
        {
            AdicionarMensagemFalhaValidacao(mensagem);
            throw new FalhaValidacaoException();
        }
    }
}
