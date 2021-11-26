
namespace AplicativoWebAcademia.regras

{
    public class PessoaRegras
    {
        public static bool VerificaFilhos(int VerificaFilhos)
        {
            if (VerificaFilhos >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool VerificaNascimento(DateTime VerificaNascimento)
        {
            if (VerificaNascimento >= new DateTime(1990, 1, 1))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool EditarPessoaInativa(string SituaçãoPessoa)
        {
            if (SituaçãoPessoa.Equals("Inativa"))
            {
                return false;
            }
            else
            { 
                return true;
            }
        }
        public static bool VerificaSalarioMin(Decimal SalarioMinPessoa)
        {
            if (SalarioMinPessoa > 1200)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool VerificaSalarioMax(Decimal SalarioMaxPessoa)
        {
            if (SalarioMaxPessoa < 13000)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool VerificaAtivo(string SituaçãoPessoa)
        {
            if (SituaçãoPessoa.Equals("Inativa"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool VerificaInativo(string SituaçãoPessoa)
        {
            if (SituaçãoPessoa.Equals("Ativo"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}