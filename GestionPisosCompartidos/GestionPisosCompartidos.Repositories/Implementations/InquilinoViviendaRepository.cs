using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Data;
using GestionPisosCompartidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionPisosCompartidos.Repositories.Implementations
{
    public class InquilinoViviendaRepository : IInquilinoViviendaRepository
    {
        private readonly AppDbContext _context;

        public InquilinoViviendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InquilinosVivienda>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _context.InquilinosViviendas
                .Where(iv => iv.ViviendaId == viviendaId)
                .Include(iv => iv.Inquilino)
                .ToListAsync();
        }

        public async Task<List<InquilinosVivienda>> GetByInquilinoIdAsync(int inquilinoId)
        {
            return await _context.InquilinosViviendas
                .Where(iv => iv.InquilinoId == inquilinoId)
                .Include(iv => iv.Vivienda)
                .ToListAsync();
        }

        public async Task<List<InquilinosVivienda>> GetActivosByViviendaIdAsync(int viviendaId)
        {
            return await _context.InquilinosViviendas
                .Where(iv => iv.ViviendaId == viviendaId && iv.Activo == true)
                .Include(iv => iv.Inquilino)
                .ToListAsync();
        }

        public async Task<InquilinosVivienda> CreateAsync(InquilinosVivienda inquilinoVivienda)
        {
            _context.InquilinosViviendas.Add(inquilinoVivienda);
            await _context.SaveChangesAsync();
            return inquilinoVivienda;
        }

        public async Task<InquilinosVivienda?> DesactivarAsync(int id)
        {
            var iv = await _context.InquilinosViviendas.FindAsync(id);
            if (iv == null) return null;

            iv.Activo = false;
            iv.FechaFin = DateOnly.FromDateTime(DateTime.UtcNow);

            await _context.SaveChangesAsync();
            return iv;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var iv = await _context.InquilinosViviendas.FindAsync(id);
            if (iv == null) return false;

            _context.InquilinosViviendas.Remove(iv);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}