using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Interfaces;

namespace GestionPisosCompartidos.Services.Implementations
{
    public class IncidenciaService : IIncidenciaService
    {
        private readonly IIncidenciaRepository _repository;

        public IncidenciaService(IIncidenciaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Incidencia>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Incidencia?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Incidencia>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _repository.GetByViviendaIdAsync(viviendaId);
        }

        public async Task<Incidencia> CreateAsync(Incidencia incidencia)
        {
            incidencia.FechaCreacion = DateTime.UtcNow;
            incidencia.Estado = "Abierta";
            return await _repository.CreateAsync(incidencia);
        }

        public async Task<Incidencia?> UpdateAsync(Incidencia incidencia)
        {
            return await _repository.UpdateAsync(incidencia);
        }

        public async Task<Incidencia?> UpdateEstadoAsync(int id, string estado)
        {
            return await _repository.UpdateEstadoAsync(id, estado);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}