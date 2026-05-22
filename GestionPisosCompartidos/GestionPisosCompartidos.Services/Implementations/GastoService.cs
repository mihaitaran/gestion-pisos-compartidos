using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Interfaces;

namespace GestionPisosCompartidos.Services.Implementations
{
    public class GastoService : IGastoService
    {
        private readonly IGastoRepository _repository;
        private readonly IInquilinoViviendaRepository _inquilinoViviendaRepository;
        private readonly IPagoRepository _pagoRepository;

        public GastoService(
            IGastoRepository repository,
            IInquilinoViviendaRepository inquilinoViviendaRepository,
            IPagoRepository pagoRepository)
        {
            _repository = repository;
            _inquilinoViviendaRepository = inquilinoViviendaRepository;
            _pagoRepository = pagoRepository;
        }

        public async Task<Gasto> CreateAsync(Gasto gasto, List<int>? inquilinosIds = null)
        {
            gasto.FechaRegistro = DateTime.UtcNow;
            var gastoCreado = await _repository.CreateAsync(gasto);

            List<InquilinosVivienda> inquilinos;

            if (inquilinosIds != null && inquilinosIds.Count > 0)
            {
                var todosInquilinos = await _inquilinoViviendaRepository.GetActivosByViviendaIdAsync(gasto.ViviendaId);
                inquilinos = todosInquilinos.Where(iv => inquilinosIds.Contains(iv.InquilinoId)).ToList();
            }
            else
            {
                inquilinos = await _inquilinoViviendaRepository.GetActivosByViviendaIdAsync(gasto.ViviendaId);
            }

            if (inquilinos.Count > 0)
            {
                var importePorPersona = gasto.ImporteTotal / inquilinos.Count;

                foreach (var iv in inquilinos)
                {
                    var pago = new Pago
                    {
                        GastoId = gastoCreado.Id,
                        InquilinoId = iv.InquilinoId,
                        Importe = importePorPersona,
                        Estado = "Pendiente"
                    };
                    await _pagoRepository.CreateAsync(pago);
                }
            }

            return gastoCreado;
        }
        public async Task<List<Gasto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Gasto?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Gasto>> GetByViviendaIdAsync(int viviendaId)
        {
            return await _repository.GetByViviendaIdAsync(viviendaId);
        }

        public async Task<Gasto?> UpdateAsync(Gasto gasto)
        {
            return await _repository.UpdateAsync(gasto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}