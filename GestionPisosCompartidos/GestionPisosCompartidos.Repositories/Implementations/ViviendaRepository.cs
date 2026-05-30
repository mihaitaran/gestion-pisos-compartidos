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
                .Include(v => v.InquilinosVivienda)
                .Include(v => v.Gastos)
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

            existente.Calle = vivienda.Calle;
            existente.Numero = vivienda.Numero;
            existente.Piso = vivienda.Piso;
            existente.Puerta = vivienda.Puerta;
            existente.Ciudad = vivienda.Ciudad;
            existente.CodigoPostal = vivienda.CodigoPostal;
            existente.Descripcion = vivienda.Descripcion;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vivienda = await _context.Viviendas
                .Include(v => v.Gastos)
                    .ThenInclude(g => g.Pagos)
                .Include(v => v.Incidencia)
                .Include(v => v.TareasCalendarios)
                .Include(v => v.Mensajes)
                .Include(v => v.InquilinosVivienda)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vivienda == null) return false;

            foreach (var gasto in vivienda.Gastos)
            {
                _context.Pagos.RemoveRange(gasto.Pagos);
            }
            _context.Gastos.RemoveRange(vivienda.Gastos);
            _context.Incidencias.RemoveRange(vivienda.Incidencia);
            _context.TareasCalendarios.RemoveRange(vivienda.TareasCalendarios);
            _context.Mensajes.RemoveRange(vivienda.Mensajes);
            _context.InquilinosViviendas.RemoveRange(vivienda.InquilinosVivienda.Where(iv => !iv.Activo));
            _context.Viviendas.Remove(vivienda);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}