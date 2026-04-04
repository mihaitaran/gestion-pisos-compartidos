using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Data;
using GestionPisosCompartidos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionPisosCompartidos.Repositories.Implementations
{
    public class PagoRepository : IPagoRepository
    {
        private readonly AppDbContext _context;

        public PagoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pago>> GetAllAsync()
        {
            return await _context.Pagos
                .Include(p => p.Gasto)
                .Include(p => p.Inquilino)
                .ToListAsync();
        }

        public async Task<Pago?> GetByIdAsync(int id)
        {
            return await _context.Pagos
                .Include(p => p.Gasto)
                .Include(p => p.Inquilino)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pago>> GetByInquilinoIdAsync(int inquilinoId)
        {
            return await _context.Pagos
                .Where(p => p.InquilinoId == inquilinoId)
                .Include(p => p.Gasto)
                .ToListAsync();
        }

        public async Task<List<Pago>> GetByGastoIdAsync(int gastoId)
        {
            return await _context.Pagos
                .Where(p => p.GastoId == gastoId)
                .Include(p => p.Inquilino)
                .ToListAsync();
        }

        public async Task<Pago> CreateAsync(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return pago;
        }

        public async Task<Pago?> UpdateEstadoAsync(int id, string estado)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return null;

            pago.Estado = estado;
            pago.FechaPago = estado == "Pagado" ? DateTime.UtcNow : null;

            await _context.SaveChangesAsync();
            return pago;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return false;

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}