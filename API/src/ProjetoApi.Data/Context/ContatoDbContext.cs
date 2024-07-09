using Microsoft.EntityFrameworkCore;
using ProjetoApi.Domain.Models;

namespace ProjetoApi.Data.Context
{
    public class ContatoDbContext : DbContext
    {

        public ContatoDbContext(DbContextOptions<ContatoDbContext> options) : base(options)
        {

        }
        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
