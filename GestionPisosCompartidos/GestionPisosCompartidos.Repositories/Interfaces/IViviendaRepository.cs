using GestionPisosCompartidos.Models.Entities;

namespace GestionPisosCompartidos.Repositories.Interfaces
{
    public interface IViviendaRepository
    {
        Task<List<Vivienda>> GetAllAsync();
        Task<Vivienda?> GetByIdAsync(int id);
        Task<List<Vivienda>> GetByPropietarioIdAsync(int propietarioId);
        Task<Vivienda> CreateAsync(Vivienda vivienda);
        Task<Vivienda?> UpdateAsync(Vivienda vivienda);
        Task<bool> DeleteAsync(int id);
    }
}