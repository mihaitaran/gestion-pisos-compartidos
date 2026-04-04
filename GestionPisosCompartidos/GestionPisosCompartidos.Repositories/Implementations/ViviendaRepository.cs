using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Data;
using GestionPisosCompartidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionPisosCompartidos.Repositories.Implementations
{
    public class ViviendaRepository : IViviendaRepository
    {
        private readonly AppDbContext _context;

        public ViviendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vivienda>> GetAllAsync()
        {
            return await _context.Viviendas
                .Include(v => v.Propietario)
                .ToListAsync();
        }

        public async Task<Vivienda?> GetByIdAsync(int id)
        {
            return await _context.Viviendas
                .Include(v => v.Propietario)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Vivienda>> GetByPropietarioIdAsync(int propietarioId)
        {
            return await _context.Viviendas
                .Where(v => v.PropietarioId == propietarioId)
                .ToListAsync();
        }

        public async Task<Vivienda> CreateAsync(Vivienda vivienda)
        {
            _context.Viviendas.Add(vivienda);
            await _context.SaveChangesAsync();
            return vivienda;
        }

        public async Task<Vivienda?> UpdateAsync(Vivienda vivienda)
        {
            var existente = await _context.Viviendas.FindAsync(vivienda.Id);
            if (existente == null) return null;

            existente.Direccion = vivienda.Direccion;
            existente.Ciudad = vivienda.Ciudad;
            existente.CodigoPostal = vivienda.CodigoPostal;
            existente.Descripcion = vivienda.Descripcion;
            existente.NumHabitaciones = vivienda.NumHabitaciones;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vivienda = await _context.Viviendas.FindAsync(id);
            if (vivienda == null) return false;

            _context.Viviendas.Remove(vivienda);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}