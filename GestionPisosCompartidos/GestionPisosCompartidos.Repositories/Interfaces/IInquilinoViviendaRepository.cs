using GestionPisosCompartidos.Models.Entities;

namespace GestionPisosCompartidos.Repositories.Interfaces
{
    public interface IInquilinoViviendaRepository
    {
        Task<List<InquilinosVivienda>> GetByViviendaIdAsync(int viviendaId);
        Task<List<InquilinosVivienda>> GetByInquilinoIdAsync(int inquilinoId);
        Task<List<InquilinosVivienda>> GetActivosByViviendaIdAsync(int viviendaId);
        Task<InquilinosVivienda> CreateAsync(InquilinosVivienda inquilinoVivienda);
        Task<InquilinosVivienda?> DesactivarAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}