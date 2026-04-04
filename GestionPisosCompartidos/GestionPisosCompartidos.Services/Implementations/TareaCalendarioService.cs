using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Interfaces;

namespace GestionPisosCompartidos.Services.Implementations
{
    public class TareaCalendarioService : ITareaCalendarioService
    {
        private readonly ITareaCalendarioRepository _repository;

        public TareaCalendarioService(ITareaCalendarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TareasCalendario>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TareasCalendario?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<TareasCalendario>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _repository.GetByViviendaIdAsync(viviendaId);
        }

        public async Task<TareasCalendario> CreateAsync(TareasCalendario tarea)
        {
            tarea.FechaCreacion = DateTime.UtcNow;
            tarea.Completada = false;
            return await _repository.CreateAsync(tarea);
        }

        public async Task<TareasCalendario?> UpdateAsync(TareasCalendario tarea)
        {
            return await _repository.UpdateAsync(tarea);
        }

        public async Task<TareasCalendario?> CompletarAsync(int id)
        {
            return await _repository.CompletarAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}