using GestionPisosCompartidos.Models.Entities;

namespace GestionPisosCompartidos.Repositories.Interfaces
{
    public interface IIncidenciaRepository
    {
        Task<List<Incidencia>> GetAllAsync();
        Task<Incidencia?> GetByIdAsync(int id);
        Task<List<Incidencia>> GetByViviendaIdAsync(int viviendaId);
        Task<Incidencia> CreateAsync(Incidencia incidencia);
        Task<Incidencia?> UpdateAsync(Incidencia incidencia);
        Task<Incidencia?> UpdateEstadoAsync(int id, string estado);
        Task<bool> DeleteAsync(int id);
    }
}