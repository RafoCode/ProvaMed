using AutoMapper;
using ProjetoApi.Domain.Models;
using ProjetoApi.Service.DTO;
using ProjetoApi.Service.ViewModel;
namespace ProjetoApi.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Contato, ContatoViewModel>().ReverseMap();
            CreateMap<ContatoDTO, ContatoViewModel>().ReverseMap();
        }
    }
}
