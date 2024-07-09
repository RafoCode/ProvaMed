using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using ProjetoApi.Data.Context;
using ProjetoApi.Data.Repository.Interfaces;
using ProjetoApi.Data.Repository;
using ProjetoApi.Service.Service.Interfaces;
using ProjetoApi.Service.Service;
using Microsoft.EntityFrameworkCore;

namespace ProjetoApi.Tests.Configuration
{
    public class TesteBase
    {
        protected ContatoDbContext _context = default!;
        protected IMapper _mapper = default!;
        protected IServiceProvider _serviceProvider = default!;
        private string DataBaseName = "DataBaseTest" + Guid.NewGuid();


        public TesteBase()
        {
            InicializarContainer();
            InicializarContexto();
        }

        private void InicializarContexto()
        {
            _context = _serviceProvider.GetRequiredService<ContatoDbContext>();
            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }


        private void InicializarContainer()
        {
            var serviceColection = new ServiceCollection();
            serviceColection.AddDbContext<ContatoDbContext>(options => options.UseInMemoryDatabase(databaseName: DataBaseName));
            serviceColection.AddScoped<IContatoRepository, ContatoRepository>();
            serviceColection.AddScoped<IContatoService, ContatoService>();
            serviceColection.AddAutoMapper(typeof(ProjetoApi.Api.Configuration.AutoMapperConfig));
            _serviceProvider = serviceColection.BuildServiceProvider();
        }
    }
}
