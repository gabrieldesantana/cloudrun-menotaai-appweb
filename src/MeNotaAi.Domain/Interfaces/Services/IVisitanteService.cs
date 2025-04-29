using MeNotaAi.Domain.Entities;

namespace MeNotaAi.Domain.Interfaces.Services
{
    public interface IVisitanteService
    {
        Task<IEnumerable<VisitanteViewModel>> SelectAllAsync();
        Task<bool> CreateAsync(VisitanteInputModel visitanteInputModel);
    }
}
