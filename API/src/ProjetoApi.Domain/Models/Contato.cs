namespace ProjetoApi.Domain.Models
{
    public class Contato : Entity
    {
        public string? NomeContato { get; set; }
        public DateTime DtNascimento { get; set; }
        public string? Sexo { get; set; }
        public bool Ativo { get; set; }
    }
}
