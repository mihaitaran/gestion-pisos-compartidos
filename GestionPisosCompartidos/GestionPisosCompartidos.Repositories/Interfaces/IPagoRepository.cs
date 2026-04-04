using GestionPisosCompartidos.Models.Entities;

namespace GestionPisosCompartidos.Repositories.Interfaces
{
    public interface IPagoRepository
    {
        Task<List<Pago>> GetAllAsync();
        Task<Pago?> GetByIdAsync(int id);
        Task<List<Pago>> GetByInquilinoIdAsync(int inquilinoId);
        Task<List<Pago>> GetByGastoIdAsync(int gastoId);
        Task<Pago> CreateAsync(Pago pago);
        Task<Pago?> UpdateEstadoAsync(int id, string estado);
        Task<bool> DeleteAsync(int id);
    }
}