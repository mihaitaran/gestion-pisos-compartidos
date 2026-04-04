using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Interfaces;

namespace GestionPisosCompartidos.Services.Implementations
{
    public class GastoService : IGastoService
    {
        private readonly IGastoRepository _repository;

        public GastoService(IGastoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Gasto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Gasto?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Gasto>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _repository.GetByViviendaIdAsync(viviendaId);
        }

        public async Task<Gasto> CreateAsync(Gasto gasto)
        {
            gasto.FechaRegistro = DateTime.UtcNow;
            return await _repository.CreateAsync(gasto);
        }

        public async Task<Gasto?> UpdateAsync(Gasto gasto)
        {
            return await _repository.UpdateAsync(gasto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}