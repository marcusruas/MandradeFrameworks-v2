using Bogus;
using MandradeFrameworks.Mensagens;
using MandradeFrameworks.Mensagens.Models;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MandradeFrameworks.Tests.UnitTests
{
    public abstract class UnitTestsClass
    {
        public UnitTestsClass()
        {
            _cancellationToken = new CancellationToken();
        }
        
        protected CancellationToken _cancellationToken;

        protected static void MensageriaDeveConterFalhaValidacao(IMensageria mensageria, string mensagem)
            => mensageria.Mensagens
                .Any(x => x.Tipo == TipoMensagem.FalhaValidacao && x.Valor == mensagem)
                .ShouldBeTrue();

        protected static void MensageriaDeveConterErro(IMensageria mensageria, string mensagem)
            => mensageria.Mensagens
                .Any(x => x.Tipo == TipoMensagem.Erro && x.Valor == mensagem)
                .ShouldBeTrue();

        protected static void MensageriaDeveConterAlerta(IMensageria mensageria, string mensagem)
            => mensageria.Mensagens
                .Any(x => x.Tipo == TipoMensagem.Alerta && x.Valor == mensagem)
                .ShouldBeTrue();

        protected static void MensageriaDeveConterInformativa(IMensageria mensageria, string mensagem)
            => mensageria.Mensagens
                .Any(x => x.Tipo == TipoMensagem.Informativa && x.Valor == mensagem)
                .ShouldBeTrue();
    }
}
