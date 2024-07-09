using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjetoApi.Service.Service.Interfaces;
using ProjetoApi.Service.ViewModel;
using ProjetoApi.Tests.Configuration;

namespace ProjetoApi.Tests.ContatoTest
{
    public class ContatoTest : TesteBase
    {
        public IContatoService _contatoService;

        public ContatoTest()
        {
            _contatoService = _serviceProvider.GetRequiredService<IContatoService>();
        }

        [Test]
        public async Task Adicionar_IdadeMenor18_Falha()
        {
            //arrange
            var createContato = new ContatoViewModel() { DtNascimento = DateTime.Parse("10-6-2020"), NomeContato = "Maria Silva", Ativo = true, Sexo = "F" };

            //act
            var resultadoAcao = await _contatoService.Add(createContato);

            //assert	
            Assert.That(resultadoAcao.IsValid, Is.False);
        }



        [Test]
        public async Task Adicionar_DtNascMaiorHj_Falha()
        {
            //arrange
            var msg = "Erro ao alterar contato!Data de nascimento é maior que a data atual";
            var createContato = new ContatoViewModel() { DtNascimento = DateTime.Now.AddDays(1), NomeContato = "Maria Silva", Ativo = true, Sexo = "F" };

            //act
            var resultadoAcao = await _contatoService.Add(createContato);

            //assert	
            Assert.That(resultadoAcao.IsValid == false && resultadoAcao.Message == msg);

        }
    }
}
