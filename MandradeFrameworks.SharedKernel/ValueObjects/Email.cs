using MandradeFrameworks.SharedKernel.Exceptions;
using System.Text.RegularExpressions;

namespace DpcaFramework.SharedKernel.ValueObjects
{
    /// <summary>
    /// Validador e armazenador de email
    /// </summary>
    public struct Email
    {
        private readonly string _value;

        private Email(string email) => _value = email;

        public static bool TryParse(string value, out Email email)
        {
            email = new Email(value);
            string pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
            return Regex.IsMatch(value, pattern);
        }

        public static Email Parse(string value)
        {
            if (TryParse(value, out var result))
                return result;
            throw new EmailInvalidoException(value);
        }

        public static implicit operator Email(string input)
            => Parse(input);

        public override string ToString() => _value;
    }
}