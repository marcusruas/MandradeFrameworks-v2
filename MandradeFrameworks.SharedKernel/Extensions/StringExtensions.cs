using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MandradeFrameworks.SharedKernel.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Recebe uma string e retorna a mesma, porém sem acentos (tais como á, ê, ò, ç...)
        /// </summary>
        /// <param name="texto">string a ser convertida</param>
        /// <returns>a string informada, porém sem acentos</returns>
        public static string RemoverAcentos(this string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return null;

            var stringBuilder = new StringBuilder();

            var stringNormalizada = texto.Normalize(NormalizationForm.FormD);
            foreach (var caractere in stringNormalizada)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(caractere);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(caractere);
            }

            return stringBuilder.ToString()
                .Normalize(NormalizationForm.FormC)
                .Trim();
        }

        /// <summary>
        /// Valida se a string passada contém espaços em branco (" ")
        /// 
        /// Retorna false caso a string esteja nula ou possua apenas espaços vazios
        /// </summary>
        public static bool ContemEspacosEmBranco(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            foreach (var caractere in text)
            {
                if (char.IsWhiteSpace(caractere))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Valida se a string passada contém acentos (tais como á, ê, ò, ç...). 
        /// 
        /// O método utiliza como comparação o método <see cref="RemoverAcentos"/>
        ///
        /// Retorna false caso a string esteja nula ou possua apenas espaços vazios
        /// </summary>
        public static bool ContemAcentos(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            var textoFormatado = text.Trim().ToUpper();
            var textoNormalizado = text.RemoverAcentos();

            return textoFormatado != textoNormalizado;
        }
    }
}
