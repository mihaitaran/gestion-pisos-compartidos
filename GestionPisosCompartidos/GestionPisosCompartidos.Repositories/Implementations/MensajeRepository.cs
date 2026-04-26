using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Data;
using GestionPisosCompartidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionPisosCompartidos.Repositories.Implementations
{
    public class MensajeRepository : IMensajeRepository
    {
        private readonly AppDbContext _context;

        public MensajeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Mensaje>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _context.Mensajes
                .Where(m => m.ViviendaId == viviendaId)
                .Include(m => m.Emisor)
                .OrderBy(m => m.FechaEnvio)
                .ToListAsync();
        }

        public async Task<Mensaje> CreateAsync(Mensaje mensaje)
        {
            _context.Mensajes.Add(mensaje);
            await _context.SaveChangesAsync();
            return mensaje;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mensaje = await _context.Mensajes.FindAsync(id);
            if (mensaje == null) return false;

            _context.Mensajes.Remove(mensaje);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}