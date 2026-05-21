using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Interfaces;

namespace GestionPisosCompartidos.Services.Implementations
{
    public class HabitacionService : IHabitacionService
    {
        private readonly IHabitacionRepository _repository;
        private readonly IViviendaRepository _viviendaRepository;

        public HabitacionService(IHabitacionRepository repository, IViviendaRepository viviendaRepository)
        {
            _repository = repository;
            _viviendaRepository = viviendaRepository;
        }

        public async Task<List<Habitacion>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _repository.GetByViviendaIdAsync(viviendaId);
        }

        public async Task<Habitacion?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Habitacion> CreateAsync(Habitacion habitacion)
        {
            var vivienda = await _viviendaRepository.GetByIdAsync(habitacion.ViviendaId);
            if (vivienda == null)
                throw new InvalidOperationException("La vivienda no existe");

            var habitacionesExistentes = await _repository.GetByViviendaIdAsync(habitacion.ViviendaId);
            if (habitacionesExistentes.Count >= vivienda.NumHabitaciones)
                throw new InvalidOperationException($"La vivienda solo tiene {vivienda.NumHabitaciones} habitaciones");

            return await _repository.CreateAsync(habitacion);
        }

        public async Task<Habitacion?> UpdateAsync(Habitacion habitacion)
        {
            return await _repository.UpdateAsync(habitacion);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var habitacion = await _repository.GetByIdAsync(id);
            if (habitacion == null) return false;

            // Comprobar si tiene inquilinos asignados
            var habitaciones = await _repository.GetByViviendaIdAsync(habitacion.ViviendaId);
            var estaHabitacion = habitaciones.FirstOrDefault(h => h.Id == id);

            if (estaHabitacion?.InquilinosVivienda != null && estaHabitacion.InquilinosVivienda.Any(iv => iv.Activo))
            {
                throw new InvalidOperationException("No se puede eliminar una habitación con un inquilino asignado");
            }

            return await _repository.DeleteAsync(id);
        }
    }
}