using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Interfaces;

namespace GestionPisosCompartidos.Services.Implementations
{
    public class MensajeService : IMensajeService
    {
        private readonly IMensajeRepository _repository;

        public MensajeService(IMensajeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Mensaje>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _repository.GetByViviendaIdAsync(viviendaId);
        }

        public async Task<Mensaje> CreateAsync(Mensaje mensaje)
        {
            mensaje.FechaEnvio = DateTime.UtcNow;
            return await _repository.CreateAsync(mensaje);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}