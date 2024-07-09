using ProjetoApi.Domain.Models;

namespace ProjetoApi.Data.Repository.Interfaces
{
    public interface IContatoRepository
    {
        Task<Contato> GetByID(Guid contatoId);
        Task<List<Contato>> GetAll();
        Task<Contato> Add(Contato contato);
        Task<Contato> Delete(Contato contato);
        Task<Contato> Alterar(Contato contato);
    }
}
