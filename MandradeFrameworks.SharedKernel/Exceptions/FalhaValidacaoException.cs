namespace MandradeFrameworks.SharedKernel.Exceptions
{
    public class FalhaValidacaoException : ControlledException
    {
        public FalhaValidacaoException()
        {
            MensagemPadrao = "Uma ou mais informações solicitadas são inválidas. Contate o Suporte para mais informações.";
        }

        public override int CodigoRetorno => 400;
        public override string MensagemPadrao { get; }
    }
}
