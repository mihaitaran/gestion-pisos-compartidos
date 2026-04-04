using GestionPisosCompartidos.Models.Entities;

namespace GestionPisosCompartidos.Services.Interfaces
{
    public interface IInquilinoViviendaService
    {
        Task<List<InquilinosVivienda>> GetByViviendaIdAsync(int viviendaId);
        Task<List<InquilinosVivienda>> GetByInquilinoIdAsync(int inquilinoId);
        Task<List<InquilinosVivienda>> GetActivosByViviendaIdAsync(int viviendaId);
        Task<InquilinosVivienda> CreateAsync(InquilinosVivienda inquilinoVivienda);
        Task<InquilinosVivienda?> DesactivarAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}