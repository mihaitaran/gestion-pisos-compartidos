using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Data;
using GestionPisosCompartidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionPisosCompartidos.Repositories.Implementations
{
    public class TareaCalendarioRepository : ITareaCalendarioRepository
    {
        private readonly AppDbContext _context;

        public TareaCalendarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TareasCalendario>> GetAllAsync()
        {
            return await _context.TareasCalendarios
                .Include(t => t.AsignadaA)
                .Include(t => t.CreadaPor)
                .Include(t => t.Vivienda)
                .ToListAsync();
        }

        public async Task<TareasCalendario?> GetByIdAsync(int id)
        {
            return await _context.TareasCalendarios
                .Include(t => t.AsignadaA)
                .Include(t => t.CreadaPor)
                .Include(t => t.Vivienda)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TareasCalendario>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _context.TareasCalendarios
                .Where(t => t.ViviendaId == viviendaId)
                .Include(t => t.AsignadaA)
                .Include(t => t.CreadaPor)
                .ToListAsync();
        }

        public async Task<TareasCalendario> CreateAsync(TareasCalendario tarea)
        {
            _context.TareasCalendarios.Add(tarea);
            await _context.SaveChangesAsync();
            return tarea;
        }

        public async Task<TareasCalendario?> UpdateAsync(TareasCalendario tarea)
        {
            var existente = await _context.TareasCalendarios.FindAsync(tarea.Id);
            if (existente == null) return null;

            existente.Titulo = tarea.Titulo;
            existente.Descripcion = tarea.Descripcion;
            existente.FechaProgramada = tarea.FechaProgramada;
            existente.AsignadaAid = tarea.AsignadaAid;
            existente.Recurrente = tarea.Recurrente;
            existente.FrecuenciaDias = tarea.FrecuenciaDias;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<TareasCalendario?> CompletarAsync(int id)
        {
            var tarea = await _context.TareasCalendarios.FindAsync(id);
            if (tarea == null) return null;

            tarea.Completada = true;

            await _context.SaveChangesAsync();
            return tarea;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tarea = await _context.TareasCalendarios.FindAsync(id);
            if (tarea == null) return false;

            _context.TareasCalendarios.Remove(tarea);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}