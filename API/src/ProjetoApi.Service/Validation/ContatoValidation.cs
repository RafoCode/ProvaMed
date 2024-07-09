using ProjetoApi.Service.ViewModel;

namespace ProjetoApi.Service.Validation
{
    public static class ContatoValidation
    {
        public static ContatoViewModel ValidarDados(ContatoViewModel obj)
        {
            var validacao = new ContatoViewModel() { Valido = true };

            if (obj.DtNascimento > DateTime.Now)
                validacao = new ContatoViewModel() { Valido = false, MsgErro = "Data de nascimento é maior que a data atual" };
            else
            {
                if (obj.Idade < 18)
                    validacao = new ContatoViewModel() { Valido = false, MsgErro = "o contato deve ser maior de idade" };
            }


            return validacao;
        }
    }
}
