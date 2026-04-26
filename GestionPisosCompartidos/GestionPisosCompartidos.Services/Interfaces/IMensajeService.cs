using GestionPisosCompartidos.Models.Entities;

namespace GestionPisosCompartidos.Services.Interfaces
{
    public interface IMensajeService
    {
        Task<List<Mensaje>> GetByViviendaIdAsync(int viviendaId);
        Task<Mensaje> CreateAsync(Mensaje mensaje);
        Task<bool> DeleteAsync(int id);
    }
}