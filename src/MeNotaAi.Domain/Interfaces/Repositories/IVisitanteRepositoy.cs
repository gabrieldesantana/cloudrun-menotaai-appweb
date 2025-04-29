using MeNotaAi.Domain.Entities;

namespace MeNotaAi.Domain.Interfaces.Repositories
{
    public interface IVisitanteRepositoy
    {
        Task<IEnumerable<Visitante>> SelectAllAsync();
        Task<bool> CreateAsync(Visitante visitante);
    }
}
