using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Data;
using GestionPisosCompartidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionPisosCompartidos.Repositories.Implementations
{
    public class HabitacionRepository : IHabitacionRepository
    {
        private readonly AppDbContext _context;

        public HabitacionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Habitacion>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _context.Habitaciones
                .Where(h => h.ViviendaId == viviendaId)
                .Include(h => h.InquilinosVivienda)
                    .ThenInclude(iv => iv.Inquilino)
                .ToListAsync();
        }

        public async Task<Habitacion?> GetByIdAsync(int id)
        {
            return await _context.Habitaciones
                .Include(h => h.Vivienda)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Habitacion> CreateAsync(Habitacion habitacion)
        {
            _context.Habitaciones.Add(habitacion);
            await _context.SaveChangesAsync();
            return habitacion;
        }

        public async Task<Habitacion?> UpdateAsync(Habitacion habitacion)
        {
            var existente = await _context.Habitaciones.FindAsync(habitacion.Id);
            if (existente == null) return null;

            existente.Numero = habitacion.Numero;
            existente.TieneBano = habitacion.TieneBano;
            existente.Descripcion = habitacion.Descripcion;
            existente.PrecioMensual = habitacion.PrecioMensual;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null) return false;

            _context.Habitaciones.Remove(habitacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}