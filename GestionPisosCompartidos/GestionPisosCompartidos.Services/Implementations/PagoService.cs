using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Interfaces;

namespace GestionPisosCompartidos.Services.Implementations
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _repository;

        public PagoService(IPagoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Pago>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Pago?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Pago>> GetByInquilinoIdAsync(int inquilinoId)
        {
            return await _repository.GetByInquilinoIdAsync(inquilinoId);
        }

        public async Task<List<Pago>> GetByGastoIdAsync(int gastoId)
        {
            return await _repository.GetByGastoIdAsync(gastoId);
        }

        public async Task<Pago> CreateAsync(Pago pago)
        {
            pago.Estado = "Pendiente";
            return await _repository.CreateAsync(pago);
        }

        public async Task<Pago?> UpdateEstadoAsync(int id, string estado)
        {
            return await _repository.UpdateEstadoAsync(id, estado);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}