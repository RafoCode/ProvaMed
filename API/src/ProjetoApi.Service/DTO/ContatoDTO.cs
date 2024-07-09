using System.ComponentModel.DataAnnotations;

namespace ProjetoApi.Service.DTO
{
    public class ContatoDTO
    {
        public string? NomeContato { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DtNascimento { get; set; }
        public string? Sexo { get; set; }
        public bool Ativo { get; set; }
    }
}
