using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Data;
using GestionPisosCompartidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionPisosCompartidos.Repositories.Implementations
{
    public class GastoRepository : IGastoRepository
    {
        private readonly AppDbContext _context;

        public GastoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Gasto>> GetAllAsync()
        {
            return await _context.Gastos
                .Include(g => g.CreadoPor)
                .Include(g => g.Vivienda)
                .ToListAsync();
        }

        public async Task<Gasto?> GetByIdAsync(int id)
        {
            return await _context.Gastos
                .Include(g => g.CreadoPor)
                .Include(g => g.Vivienda)
                .Include(g => g.Pagos)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<Gasto>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _context.Gastos
                .Where(g => g.ViviendaId == viviendaId)
                .Include(g => g.CreadoPor)
                .Include(g => g.Pagos)
                .ToListAsync();
        }

        public async Task<Gasto> CreateAsync(Gasto gasto)
        {
            _context.Gastos.Add(gasto);
            await _context.SaveChangesAsync();
            return gasto;
        }

        public async Task<Gasto?> UpdateAsync(Gasto gasto)
        {
            var existente = await _context.Gastos.FindAsync(gasto.Id);
            if (existente == null) return null;

            existente.Concepto = gasto.Concepto;
            existente.Categoria = gasto.Categoria;
            existente.ImporteTotal = gasto.ImporteTotal;
            existente.FechaGasto = gasto.FechaGasto;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null) return false;

            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}