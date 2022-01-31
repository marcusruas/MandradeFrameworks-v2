using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MandradeFrameworks.SharedKernel.ValueObjects
{
    public class Cpf : IEquatable<Cpf>
    {
        /// <summary>
        /// Cria a instância do objeto de <see cref="Cpf"/> caso o CPF informado esteja incorreto por quaisquer motivos, será lançada uma exceção
        /// do tipo <see cref="ArgumentException"/> com a mensagem de erro
        /// </summary>
        /// <param name="cpf">O valor a ser validado e usado para criar o cpf</param>
        public Cpf(string cpf)
        {
            ValidarCpf(cpf);

            ValorNumerico = cpf;
            ValorFormatado = cpf;
        }

        /// <summary>
        /// Valor do cpf com pontuação (ex: "123.456.789-00")
        /// </summary>
        public string ValorFormatado { get => _valorFormatado; set => DefinirCpfFormatado(value); }
        private string _valorFormatado { get; set; }
        /// <summary>
        /// Valor do cpf com pontuação (ex: "12345678900")
        /// </summary>
        public string ValorNumerico { get => _valorNumerico; set => DefinirCpfNumerico(value); }
        private string _valorNumerico { get; set; } 

        private void ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                throw new ArgumentException("Formato do CPF inválido. CPF deve conter 11 números.");

            if(!DigitosFinaisValidos(cpf))
                throw new ArgumentException("Cpf inválido. os últimos 2 dígitos do CPF devem condizer com os primeiros 9.");
        }

        private bool DigitosFinaisValidos(string cpf)
        {
            int primeiroDigito = int.Parse(cpf[9].ToString());
            int segundoDigito = int.Parse(cpf[10].ToString());
            int somaDigitos = 0;
            bool primeiroDigitoValido;
            bool segundoDigitoValido;
            int restoDivisao;

            for (int i = 1; i < (cpf.Length - 2); i++)
            {
                int digito = int.Parse(cpf[i - 1].ToString());
                somaDigitos += digito * (11 - i);
            }
            restoDivisao = (somaDigitos * 10) % 11;
            restoDivisao = restoDivisao == 10 ? 0 : restoDivisao;
            primeiroDigitoValido = primeiroDigito == restoDivisao;

            somaDigitos = 0;

            for (int i = 1; i < cpf.Length; i++)
            {
                int digito = int.Parse(cpf[i - 1].ToString());
                somaDigitos += digito * (12 - i);
            }
            restoDivisao = (somaDigitos * 10) % 11;
            restoDivisao = restoDivisao == 10 ? 0 : restoDivisao;
            segundoDigitoValido = segundoDigito == restoDivisao;

            return primeiroDigitoValido && segundoDigitoValido;
        }

        private string DefinirCpfNumerico(string cpf)
        {
            var numeroCpf = cpf.Where(c => char.IsNumber(c)).ToArray();
            return new string(numeroCpf);
        }

        private string DefinirCpfFormatado(string cpf)
        {
            var numeroCpf = DefinirCpfNumerico(cpf);
            string formato = @"(\d{3})(\d{3})(\d{3})(\d{2})";
            return Regex.Replace(numeroCpf, formato, "$1.$2.$3-$4");
        }

        /// <summary>
        /// Cria uma instância do tipo <see cref="Cpf" /> a partir de uma string
        /// <summary>
        public static implicit operator Cpf(string input)
            => new Cpf(input.ToString());
        /// <summary>
        /// Cria uma instância do tipo <see cref="Cpf" /> a partir de um número inteiro
        /// <summary>
        public static implicit operator Cpf(int input)
            => new Cpf(input.ToString());
        /// <summary>
        /// Cria uma instância do tipo <see cref="Cpf" /> a partir de um número longo
        /// <summary>
        public static implicit operator Cpf(long input)
            => new Cpf(input.ToString());

        public bool Equals(Cpf other)
            => ValorNumerico == other.ValorNumerico;
    }
}
