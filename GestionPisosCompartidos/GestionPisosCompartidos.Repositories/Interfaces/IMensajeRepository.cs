using GestionPisosCompartidos.Models.Entities;

namespace GestionPisosCompartidos.Repositories.Interfaces
{
    public interface IMensajeRepository
    {
        Task<List<Mensaje>> GetByViviendaIdAsync(int viviendaId);
        Task<Mensaje> CreateAsync(Mensaje mensaje);
        Task<bool> DeleteAsync(int id);
    }
}