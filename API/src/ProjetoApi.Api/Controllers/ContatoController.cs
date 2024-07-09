using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoApi.Service.DTO;
using ProjetoApi.Service.Results;
using ProjetoApi.Service.Service.Interfaces;
using ProjetoApi.Service.ViewModel;

namespace ProjetoApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {

        private readonly IContatoService _contatoService;
        private readonly IMapper _mapper;

        //construtor
        public ContatoController(IContatoService contato, IMapper mapper)
        {
            _contatoService = contato;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> ObterTodos()
        {

            return GetIActionResult(await _contatoService.GetAll());

        }

        [HttpGet("ObterTodosDesativados")]
        public async Task<IActionResult> ObterTodosDesativados()
        {

            return GetIActionResult(await _contatoService.GetAllDesativados());

        }

        [HttpGet("RetornarPorId/{id:guid}")]
        public async Task<IActionResult> RetornaPorId([FromRoute] Guid id)
        {

            return GetIActionResult(await _contatoService.GetByID(id));

        }

        [HttpPost("Incluir")]
        public async Task<IActionResult> Incluir([FromBody] ContatoDTO contato)
        {

            return GetIActionResult(await _contatoService.Add(_mapper.Map<ContatoViewModel>(contato)));

        }

        [HttpPut("Alterar/{id:guid}")]
        public async Task<IActionResult> Alterar(Guid id, [FromBody] ContatoDTO obj)
        {

            return GetIActionResult(await _contatoService.Alterar(id, obj));

        }

        [HttpPut("Desativar/{id:guid}")]
        public async Task<IActionResult> Desativar([FromRoute] Guid id)
        {

            return GetIActionResult(await _contatoService.Desativar(id));

        }

        [HttpPut("Ativar/{id:guid}")]
        public async Task<IActionResult> Ativar([FromRoute] Guid id)
        {

            return GetIActionResult(await _contatoService.Ativar(id));

        }

        [HttpDelete("Excluir/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            return GetIActionResult(await _contatoService.Delete(id));

        }

        private IActionResult GetIActionResult(Result result)
        {
            if (result == null)
            {
                return StatusCode((int)StatusCodeResultEnum.InternalServerError);
            }
            return StatusCode((int)result.StatusCode, result);
        }

        private IActionResult GetIActionResult<T>(Result<T> result)
        {
            if (result == null)
            {
                return StatusCode((int)StatusCodeResultEnum.InternalServerError);
            }
            if (result.StatusCode == StatusCodeResultEnum.NoContent)
            {
                return StatusCode((int)result.StatusCode);
            }
            return StatusCode((int)result.StatusCode, result);
        }
    }
}

