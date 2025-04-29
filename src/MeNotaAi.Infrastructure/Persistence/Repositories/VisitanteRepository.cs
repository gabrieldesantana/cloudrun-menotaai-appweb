using MeNotaAi.Domain.Entities;
using MeNotaAi.Domain.Interfaces.Repositories;
using MeNotaAi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MeNotaAi.Infrastructure.Persistence.Repositories
{
    public class VisitanteRepository : IVisitanteRepositoy
    {
        private readonly AppDbContext _context;
        public VisitanteRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Visitante>> SelectAllAsync()
        {
            return await _context.Visitantes.ToListAsync();
        }

        public async Task<bool> CreateAsync(Visitante visitante)
        {
            try
            {
                await _context.Visitantes.AddAsync(visitante);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
