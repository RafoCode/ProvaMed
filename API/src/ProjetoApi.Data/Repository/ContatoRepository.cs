using Microsoft.EntityFrameworkCore;
using ProjetoApi.Data.Context;
using ProjetoApi.Data.Repository.Interfaces;
using ProjetoApi.Domain.Models;

namespace ProjetoApi.Data.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly ContatoDbContext _context;
        public ContatoRepository(ContatoDbContext context)
        {
            _context = context;
        }
        public async Task<List<Contato>> GetAll()
        {
            return await _context.Contatos.ToListAsync();
        }
        public async Task<Contato> GetByID(Guid contatoId)
        {
            return await _context.Contatos.FirstOrDefaultAsync(c => c.Id == contatoId);
        }

        public async Task<Contato> Add(Contato contato)
        {
            await _context.Contatos.AddAsync(contato);
            await _context.SaveChangesAsync();
            return contato;
        }

        public async Task<Contato> Alterar(Contato contato)
        {
            _context.Attach(contato);
            _context.Entry(contato).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return contato;
        }

        public async Task<Contato> Delete(Contato contato)
        {

            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();
            return contato;

        }
    }
}
