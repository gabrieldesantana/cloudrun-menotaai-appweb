using MeNotaAi.Domain.Entities;
using MeNotaAi.Domain.Interfaces.Repositories;
using MeNotaAi.Domain.Interfaces.Services;

namespace MeNotaAi.Application.Services
{
    public class VisitanteService : IVisitanteService
    {
        private readonly IVisitanteRepositoy _repository;

        public VisitanteService(IVisitanteRepositoy repository)
            => (_repository) = (repository);

        public async Task<bool> CreateAsync(VisitanteInputModel visitanteInputModel)
        {
            var visitante = Visitante.Create(visitanteInputModel.Nome);
            return await _repository.CreateAsync(visitante);
        }

        public async Task<IEnumerable<VisitanteViewModel>> SelectAllAsync()
        {
            var visitantesViewModel = await _repository.SelectAllAsync();
            return visitantesViewModel.Select(v => new VisitanteViewModel(v.Nome!, v.DataVisita));
        }
    }
}
