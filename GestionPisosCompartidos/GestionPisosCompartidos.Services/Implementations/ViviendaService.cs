using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Interfaces;

namespace GestionPisosCompartidos.Services.Implementations
{
    public class ViviendaService : IViviendaService
    {
        private readonly IViviendaRepository _repository;

        public ViviendaService(IViviendaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Vivienda>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Vivienda?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Vivienda>> GetByPropietarioIdAsync(int propietarioId)
        {
            return await _repository.GetByPropietarioIdAsync(propietarioId);
        }

        public async Task<Vivienda> CreateAsync(Vivienda vivienda)
        {
            vivienda.FechaCreacion = DateTime.UtcNow;
            return await _repository.CreateAsync(vivienda);
        }

        public async Task<Vivienda?> UpdateAsync(Vivienda vivienda)
        {
            return await _repository.UpdateAsync(vivienda);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vivienda = await _repository.GetByIdAsync(id);
            if (vivienda == null) return false;

            if (vivienda.InquilinosVivienda != null && vivienda.InquilinosVivienda.Any(iv => iv.Activo))
            {
                throw new InvalidOperationException("No se puede eliminar una vivienda con inquilinos activos");
            }

            if (vivienda.Habitaciones != null && vivienda.Habitaciones.Count > 0)
            {
                throw new InvalidOperationException("No se puede eliminar una vivienda con habitaciones. Elimina las habitaciones primero");
            }

            if (vivienda.Gastos != null && vivienda.Gastos.Count > 0)
            {
                throw new InvalidOperationException("No se puede eliminar una vivienda con gastos asociados");
            }

            return await _repository.DeleteAsync(id);
        }
    }
}