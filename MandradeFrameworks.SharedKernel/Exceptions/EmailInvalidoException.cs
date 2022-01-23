namespace MandradeFrameworks.SharedKernel.Exceptions
{
    public class EmailInvalidoException : ControlledException
    {
        public EmailInvalidoException(string emailInvalido)
        {
            MensagemPadrao = $"O valor {emailInvalido} não é um e-mail válido";
        }

        public override int CodigoRetorno => 400;
        public override string MensagemPadrao { get; }
    }
}
