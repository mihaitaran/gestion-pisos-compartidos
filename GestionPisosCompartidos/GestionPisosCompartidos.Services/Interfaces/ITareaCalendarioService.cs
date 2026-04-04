using GestionPisosCompartidos.Models.Entities;

namespace GestionPisosCompartidos.Services.Interfaces
{
    public interface ITareaCalendarioService
    {
        Task<List<TareasCalendario>> GetAllAsync();
        Task<TareasCalendario?> GetByIdAsync(int id);
        Task<List<TareasCalendario>> GetByViviendaIdAsync(int viviendaId);
        Task<TareasCalendario> CreateAsync(TareasCalendario tarea);
        Task<TareasCalendario?> UpdateAsync(TareasCalendario tarea);
        Task<TareasCalendario?> CompletarAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}