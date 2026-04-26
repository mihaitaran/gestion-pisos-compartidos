using GestionPisosCompartidos.Models.Entities;

namespace GestionPisosCompartidos.Repositories.Interfaces
{
    public interface IHabitacionRepository
    {
        Task<List<Habitacion>> GetByViviendaIdAsync(int viviendaId);
        Task<Habitacion?> GetByIdAsync(int id);
        Task<Habitacion> CreateAsync(Habitacion habitacion);
        Task<Habitacion?> UpdateAsync(Habitacion habitacion);
        Task<bool> DeleteAsync(int id);
    }
}