using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Interfaces;

namespace GestionPisosCompartidos.Services.Implementations
{
    public class InquilinoViviendaService : IInquilinoViviendaService
    {
        private readonly IInquilinoViviendaRepository _repository;

        public InquilinoViviendaService(IInquilinoViviendaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InquilinosVivienda>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _repository.GetByViviendaIdAsync(viviendaId);
        }

        public async Task<List<InquilinosVivienda>> GetByInquilinoIdAsync(int inquilinoId)
        {
            return await _repository.GetByInquilinoIdAsync(inquilinoId);
        }

        public async Task<List<InquilinosVivienda>> GetActivosByViviendaIdAsync(int viviendaId)
        {
            return await _repository.GetActivosByViviendaIdAsync(viviendaId);
        }

        public async Task<InquilinosVivienda> CreateAsync(InquilinosVivienda inquilinoVivienda)
        {
            var asignaciones = await _repository.GetByInquilinoIdAsync(inquilinoVivienda.InquilinoId);
            var tieneActiva = asignaciones.Any(a => a.Activo == true);

            if (tieneActiva)
            {
                throw new InvalidOperationException("El inquilino ya tiene una vivienda activa asignada");
            }

            inquilinoVivienda.Activo = true;
            inquilinoVivienda.FechaInicio = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _repository.CreateAsync(inquilinoVivienda);
        }

        public async Task<InquilinosVivienda?> DesactivarAsync(int id)
        {
            return await _repository.DesactivarAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}