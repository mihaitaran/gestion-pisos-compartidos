using GestionPisosCompartidos.Models.Entities;

namespace GestionPisosCompartidos.Services.Interfaces
{
    public interface IGastoService
    {
        Task<List<Gasto>> GetAllAsync();
        Task<Gasto?> GetByIdAsync(int id);
        Task<List<Gasto>> GetByViviendaIdAsync(int viviendaId);
        Task<Gasto> CreateAsync(Gasto gasto);
        Task<Gasto?> UpdateAsync(Gasto gasto);
        Task<bool> DeleteAsync(int id);
    }
}