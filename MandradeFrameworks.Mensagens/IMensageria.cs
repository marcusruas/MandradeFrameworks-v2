using MandradeFrameworks.Mensagens.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Mensagens
{
    public interface IMensageria
    {
        /// <summary>
        /// Retorna em um objeto inalterável as mensagens gravadas
        /// </summary>
        IEnumerable<Mensagem> Mensagens { get; }
        /// <summary>
        /// Adiciona uma mensagem com o tipo <see cref="TipoMensagem.Informativa"/>
        /// </summary>
        /// <param name="mensagem">texto da mensagem</param>
        void AdicionarMensagemInformativa(string mensagem);
        /// <summary>
        /// Adiciona uma mensagem com o tipo <see cref="TipoMensagem.Alerta"/>
        /// </summary>
        /// <param name="mensagem">texto da mensagem</param>
        void AdicionarMensagemAlerta(string mensagem);
        /// <summary>
        /// Adiciona uma mensagem com o tipo <see cref="TipoMensagem.FalhaValidacao"/>
        /// </summary>
        /// <param name="mensagem">texto da mensagem</param>
        void AdicionarMensagemFalhaValidacao(string mensagem);
        /// <summary>
        /// Adiciona uma mensagem com o tipo <see cref="TipoMensagem.Erro"/>
        /// </summary>
        /// <param name="mensagem">texto da mensagem</param>
        void AdicionarMensagemErro(string mensagem);

        /// <summary>
        /// Valida se entre as mensagens gravadas há uma cujo TipoMensagem seja <see cref="TipoMensagem.Informativa"/>
        /// </summary>
        bool PossuiInformativas();
        /// <summary>
        /// Valida se entre as mensagens gravadas há uma cujo TipoMensagem seja <see cref="TipoMensagem.Alerta"/>
        /// </summary>
        bool PossuiAlertas();
        /// <summary>
        /// Valida se entre as mensagens gravadas há uma cujo TipoMensagem seja <see cref="TipoMensagem.FalhaValidacao"/>
        /// </summary>
        bool PossuiFalhasValidacao();
        /// <summary>
        /// Valida se entre as mensagens gravadas há uma cujo TipoMensagem seja <see cref="TipoMensagem.Erro"/>
        /// </summary>
        bool PossuiErros();
    }
}
