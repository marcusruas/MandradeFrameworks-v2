using MandradeFrameworks.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.SharedKernel.ValueObjects
{
    /// <summary>
    /// Struct que representa a funcional de um funcionário
    /// <summary>
    public struct Funcional : IEquatable<Funcional>
    {
        private readonly string _value;
        /// <summary>
        /// Cria uma nova instância da struct <see cref="Funcional" /> a partir de uma string
        /// <summary>
        private Funcional(string value) => _value = value;
        /// <summary>
        /// Converte uma string para o tipo <see cref="Funcional" />. Caso não seja possível lança exceção FuncionalInvalidaException
        /// <summary>
        public static Funcional Parse(string value)
        {
            if (TryParse(value, out var result))
                return result;
            throw new FuncionalInvalidaException();
        }
        /// <summary>
        /// Converte um número inteiro para o tipo <see cref="Funcional" />. Caso não seja possível lança exceção FuncionalInvalidaException
        /// <summary>
        public static Funcional Parse(int value)
        {
            if (TryParse(value.ToString("D9"), out var result))
                return result;
            throw new FuncionalInvalidaException();
        }
        /// <summary>
        /// Converte um número longo para o tipo <see cref="Funcional" />. Caso não seja possível lança exceção FuncionalInvalidaException
        /// <summary>
        public static Funcional Parse(long value)
        {
            if (TryParse(value.ToString("D9"), out var result))
                return result;
            throw new FuncionalInvalidaException();
        }
        /// <summary>
        /// Tenta converter uma string para o tipo <see cref="Funcional" />
        /// <summary>
        public static bool TryParse(string value, out Funcional funcional)
        {
            funcional = new Funcional(value);
            if (string.IsNullOrWhiteSpace(value))
                return false;
            if (value.Length != 9)
                return false;
            return true;
        }
        /// <summary>
        /// Retorna o valor da funcional
        /// <summary>
        public override string ToString() => _value;
        /// <summary>
        /// Retorna uma funcional no formado 033.333.953
        /// <summary>
        public string ToFormattedFuncional()
            => _value.Insert(3, ".").Insert(7, ".");
        /// <summary>
        /// Compara se duas funcionais se elas são iguais
        /// <summary>
        public bool Equals(Funcional other)
            => other.ToString() == _value;
        /// <summary>
        /// Compara se um objeto é igual a uma funcional
        /// <summary>
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            Funcional f = Parse(obj.ToString());
            return f._value == _value;
        }
        /// <summary>
        /// Retorna o hashcode do objeto
        /// <summary>
        public override int GetHashCode()
        {
            return -1939223833 + EqualityComparer<string>.Default.GetHashCode(_value);
        }
        /// <summary>
        /// Cria uma instância do tipo <see cref="Funcional" /> a partir de uma string
        /// Funcional f = "033333953"
        /// <summary>
        public static implicit operator Funcional(string input)
            => Parse(input);
        /// <summary>
        /// Cria uma instância do tipo <see cref="Funcional" /> a partir de um número inteiro
        /// Funcional f = 33333953
        /// <summary>
        public static implicit operator Funcional(int input)
            => Parse(input);
        /// <summary>
        /// Cria uma instância do tipo <see cref="Funcional" /> a partir de um número longo
        /// Funcional f = 33333953
        /// <summary>
        public static implicit operator Funcional(long input)
            => Parse(input);
        /// <summary>
        /// Override do operarador ==
        /// <summary>
        public static bool operator ==(Funcional left, Funcional right)
            => left.Equals(right.ToString());
        /// <summary>
        /// Override do operarador !=
        /// <summary>
        public static bool operator !=(Funcional left, Funcional right)
            => !left.Equals(right.ToString());
    }
}
