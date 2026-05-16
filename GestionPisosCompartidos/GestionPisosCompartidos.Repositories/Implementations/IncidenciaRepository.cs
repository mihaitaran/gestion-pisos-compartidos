using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Data;
using GestionPisosCompartidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionPisosCompartidos.Repositories.Implementations
{
    public class IncidenciaRepository : IIncidenciaRepository
    {
        private readonly AppDbContext _context;

        public IncidenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Incidencia>> GetAllAsync()
        {
            return await _context.Incidencias
                .Include(i => i.ReportadaPor)
                .Include(i => i.Vivienda)
                .ToListAsync();
        }

        public async Task<Incidencia?> GetByIdAsync(int id)
        {
            return await _context.Incidencias
                .Include(i => i.ReportadaPor)
                .Include(i => i.Vivienda)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Incidencia>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _context.Incidencias
                .Where(i => i.ViviendaId == viviendaId)
                .Include(i => i.Vivienda)
                .Include(i => i.ReportadaPor)
                .ToListAsync();
        }

        public async Task<Incidencia> CreateAsync(Incidencia incidencia)
        {
            _context.Incidencias.Add(incidencia);
            await _context.SaveChangesAsync();
            return incidencia;
        }

        public async Task<Incidencia?> UpdateAsync(Incidencia incidencia)
        {
            var existente = await _context.Incidencias.FindAsync(incidencia.Id);
            if (existente == null) return null;

            existente.Titulo = incidencia.Titulo;
            existente.Descripcion = incidencia.Descripcion;
            existente.Prioridad = incidencia.Prioridad;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<Incidencia?> UpdateEstadoAsync(int id, string estado)
        {
            var incidencia = await _context.Incidencias.FindAsync(id);
            if (incidencia == null) return null;

            incidencia.Estado = estado;
            if (estado == "Resuelta" || estado == "Cerrada")
                incidencia.FechaResolucion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return incidencia;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var incidencia = await _context.Incidencias.FindAsync(id);
            if (incidencia == null) return false;

            _context.Incidencias.Remove(incidencia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}