using AutoMapper;
using ProjetoApi.Data.Repository.Interfaces;
using ProjetoApi.Domain.Models;
using ProjetoApi.Service.DTO;
using ProjetoApi.Service.Results;
using ProjetoApi.Service.Service.Interfaces;
using ProjetoApi.Service.Validation;
using ProjetoApi.Service.ViewModel;

namespace ProjetoApi.Service.Service
{

    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IMapper _mapper;

        public ContatoService(IContatoRepository contatoRepository, IMapper mapper)
        {
            _contatoRepository = contatoRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ContatoViewModel>>> GetAll()
        {

            var result = new Result<IEnumerable<ContatoViewModel>>();

            try
            {
                var lista = _mapper.Map<List<ContatoViewModel>>(await _contatoRepository.GetAll());
                var listaWhere = lista.Where(p => p.Ativo == true);
                if (listaWhere.Any())
                {
                    result.Data = listaWhere;
                    result.StatusCode = StatusCodeResultEnum.Ok;
                    result.Message = "Retornando Todos os Contatos Ativos";

                }
                else
                {

                    result.StatusCode = StatusCodeResultEnum.NoContent;

                }
            }
            catch (Exception ex)
            {

                result.StatusCode = StatusCodeResultEnum.InternalServerError;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;

        }

        public async Task<Result<IEnumerable<ContatoViewModel>>> GetAllDesativados()
        {

            var result = new Result<IEnumerable<ContatoViewModel>>();

            try
            {
                var lista = _mapper.Map<List<ContatoViewModel>>(await _contatoRepository.GetAll());
                var listaWhere = lista.Where(p => p.Ativo == false);
                if (listaWhere.Any())
                {
                    result.Data = listaWhere;
                    result.StatusCode = StatusCodeResultEnum.Ok;
                    result.Message = "Retornando Todos os Contatos Inativos";

                }
                else
                {

                    result.StatusCode = StatusCodeResultEnum.NoContent;
                    
                }
            }
            catch (Exception ex)
            {

                result.StatusCode = StatusCodeResultEnum.InternalServerError;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;

        }

        public async Task<Result<ContatoViewModel>> GetByID(Guid contatoId)
        {
            var result = new Result<ContatoViewModel>();


            try
            {
                var objContato = await _contatoRepository.GetByID(contatoId);
                if (objContato == null)
                {
                    result.Message = "Nenhum Contato Encontrado";
                    result.IsValid = false;
                    result.StatusCode = StatusCodeResultEnum.NotFound;
                }
                else if (objContato != null && !objContato.Ativo)
                {
                    result.IsValid = false;
                    result.Message = "O contato solicitado está inativo";
                    result.StatusCode = StatusCodeResultEnum.NotFound;
                }
                else
                {
                    result.Data = _mapper.Map<ContatoViewModel>(objContato);
                    result.StatusCode = StatusCodeResultEnum.Ok;
                }

            }
            catch (Exception ex)
            {

                result.StatusCode = StatusCodeResultEnum.InternalServerError;
                result.IsValid = false;
                result.Message = ex.Message;
            }
            return result;


        }

        public async Task<Result<ContatoViewModel>> Add(ContatoViewModel contato)
        {
            var result = new Result<ContatoViewModel>();
            try
            {
                var objValidacao = ContatoValidation.ValidarDados(contato);

                if (objValidacao.Valido)
                {

                    var objInclusao = _mapper.Map<Contato>(contato);
                    var resposeAdd = await _contatoRepository.Add(objInclusao);
                    result.Data = _mapper.Map<ContatoViewModel>(resposeAdd);
                    result.IsValid = true;
                    result.StatusCode = StatusCodeResultEnum.Created;

                }
                else
                {
                    result.IsValid = false;
                    result.Message = "Erro ao Incluir o Contato!" + objValidacao.MsgErro;
                    result.StatusCode = StatusCodeResultEnum.BadRequest;
                }
            }
            catch (Exception ex)
            {

                result.StatusCode = StatusCodeResultEnum.InternalServerError;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<Result<ContatoViewModel>> Alterar(Guid id, ContatoDTO contato)
        {

            var result = new Result<ContatoViewModel>();

            try
            {
                var objAlteracao = await _contatoRepository.GetByID(id);
                if (objAlteracao == null)
                {
                    result.Message = "Nenhum Contato Encontrado";
                    result.IsValid = false;
                    result.StatusCode = StatusCodeResultEnum.NotFound;
                }
                else if (objAlteracao != null && !objAlteracao.Ativo)
                {
                    result.Message = "O Contato Solicitado está inativo";
                    result.IsValid = true;
                    result.StatusCode = StatusCodeResultEnum.Ok;
                }
                else
                {
                    objAlteracao.NomeContato = contato.NomeContato;
                    objAlteracao.DtNascimento = contato.DtNascimento;
                    objAlteracao.Sexo = contato.Sexo;
                    objAlteracao.Ativo = contato.Ativo;

                    var objRetorno = _mapper.Map<ContatoViewModel>(objAlteracao);

                    var objValidacao = ContatoValidation.ValidarDados(objRetorno);
                    if (objValidacao.Valido)
                    {
                        await _contatoRepository.Alterar(objAlteracao);
                        result.Data = _mapper.Map<ContatoViewModel>(objAlteracao);
                        result.StatusCode = StatusCodeResultEnum.Ok;

                    }
                    else
                    {
                        result.IsValid = false;
                        result.Message = "Erro ao alterar contato!" + objValidacao.MsgErro;
                        result.StatusCode = StatusCodeResultEnum.BadRequest;

                    }

                }
            }
            catch (Exception ex)
            {
                result.StatusCode = StatusCodeResultEnum.InternalServerError;
                result.IsValid = false;
                result.Message = ex.Message;

            }


            return result;
        }

        public async Task<Result<ContatoViewModel>> Delete(Guid id)
        {
            var result = new Result<ContatoViewModel>();

            try
            {
                var objDelecao = await _contatoRepository.GetByID(id);
                if (objDelecao == null)
                {
                    result.Message = "Nenhum Contato Encontrado";
                    result.IsValid = false;
                    result.StatusCode = StatusCodeResultEnum.NotFound;

                }
                else
                {
                    await _contatoRepository.Delete(objDelecao);
                    result.Data = _mapper.Map<ContatoViewModel>(objDelecao);
                    result.StatusCode = StatusCodeResultEnum.Ok;
                    result.Message = "Contato Apagado da base!";

                }
            }
            catch (Exception ex)
            {
                result.StatusCode = StatusCodeResultEnum.InternalServerError;
                result.IsValid = false;
                result.Message = ex.Message;

            }

            return result;
        }

        public async Task<Result<ContatoViewModel>> Desativar(Guid id)
        {
            var result = new Result<ContatoViewModel>();

            try
            {
                var objContato = await _contatoRepository.GetByID(id);
                if (objContato == null)
                {
                    result.Message = "Nenhum Contato Encontrado";
                    result.IsValid = false;
                    result.StatusCode = StatusCodeResultEnum.NotFound;

                }
                else if (!objContato.Ativo)
                {
                    result.Message = "O Contato Solicitado já está inativo";
                    result.IsValid = true;
                    result.StatusCode = StatusCodeResultEnum.Ok;

                }
                else
                {
                    objContato.Ativo = false;
                    await _contatoRepository.Alterar(objContato);
                    result.Data = _mapper.Map<ContatoViewModel>(objContato);
                    result.StatusCode = StatusCodeResultEnum.Ok;
                    result.Message = "Contato Inativado!";

                }
            }
            catch (Exception ex)
            {
                result.StatusCode = StatusCodeResultEnum.InternalServerError;
                result.IsValid = false;
                result.Message = ex.Message;

            }

            return result;
        }

        public async Task<Result<ContatoViewModel>> Ativar(Guid id)
        {
            var result = new Result<ContatoViewModel>();

            try
            {
                var objContato = await _contatoRepository.GetByID(id);
                if (objContato == null)
                {
                    result.Message = "Nenhum Contato Encontrado";
                    result.IsValid = false;
                    result.StatusCode = StatusCodeResultEnum.NotFound;

                }
                else if (objContato.Ativo)
                {
                    result.Message = "O Contato Solicitado já está Ativo!";
                    result.IsValid = true;
                    result.StatusCode = StatusCodeResultEnum.Ok;

                }
                else
                {
                    objContato.Ativo = true;
                    await _contatoRepository.Alterar(objContato);
                    result.Data = _mapper.Map<ContatoViewModel>(objContato);
                    result.StatusCode = StatusCodeResultEnum.Ok;
                    result.Message = "Contato Ativado!";

                }
            }
            catch (Exception ex)
            {
                result.StatusCode = StatusCodeResultEnum.InternalServerError;
                result.IsValid = false;
                result.Message = ex.Message;

            }

            return result;
        }

    }
}
