using System.ComponentModel.DataAnnotations;

namespace ProjetoApi.Service.ViewModel
{
    public class ContatoViewModel
    {
        public Guid Id { get; set; }
        public string? NomeContato { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DtNascimento { get; set; }
        public string? Sexo { get; set; }
        public bool Ativo { get; set; }
        public int Idade
        {
            get
            {
                // Calcula a idade subtraindo o ano atual do ano de nascimento
                int idade = DateTime.Today.Year - DtNascimento.Year;

                // Se a data de hoje for antes do aniversário deste ano, subtraia 1 da idade
                if (DtNascimento.Date > DateTime.Today.AddYears(-idade))
                    idade--;

                return idade;
            }
        }
        public string MsgErro { get; set; }
        public bool Valido { get; set; } = true;
    }
}
