using Microsoft.EntityFrameworkCore;
using ProjetoApi.Data.Context;
using ProjetoApi.Data.Repository;
using ProjetoApi.Data.Repository.Interfaces;
using ProjetoApi.Service.Service;
using ProjetoApi.Service.Service.Interfaces;

namespace ProjetoApi.Api.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContatoDbContext>(options =>
                                                        options.UseSqlServer(configuration
                                                        .GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {
            repositories.AddScoped<IContatoRepository, ContatoRepository>();
            return repositories;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IContatoService, ContatoService>();
            return services;
        }
    }
}
