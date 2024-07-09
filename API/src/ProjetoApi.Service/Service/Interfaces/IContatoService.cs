using ProjetoApi.Service.DTO;
using ProjetoApi.Service.Results;
using ProjetoApi.Service.ViewModel;

namespace ProjetoApi.Service.Service.Interfaces
{
    public interface IContatoService
    {
        Task<Result<ContatoViewModel>> GetByID(Guid contatoId);
        Task<Result<IEnumerable<ContatoViewModel>>> GetAll();
        Task<Result<IEnumerable<ContatoViewModel>>> GetAllDesativados();
        Task<Result<ContatoViewModel>> Add(ContatoViewModel contato);
        Task<Result<ContatoViewModel>> Delete(Guid id);
        Task<Result<ContatoViewModel>> Alterar(Guid id, ContatoDTO contato);
        Task<Result<ContatoViewModel>> Desativar(Guid id);
        Task<Result<ContatoViewModel>> Ativar(Guid id);

    }
}
