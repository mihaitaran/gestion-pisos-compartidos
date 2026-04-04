using GestionPisosCompartidos.Models.Entities;

namespace GestionPisosCompartidos.Services.Interfaces
{
    public interface IViviendaService
    {
        Task<List<Vivienda>> GetAllAsync();
        Task<Vivienda?> GetByIdAsync(int id);
        Task<List<Vivienda>> GetByPropietarioIdAsync(int propietarioId);
        Task<Vivienda> CreateAsync(Vivienda vivienda);
        Task<Vivienda?> UpdateAsync(Vivienda vivienda);
        Task<bool> DeleteAsync(int id);
    }
}