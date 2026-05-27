using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Interfaces;

namespace GestionPisosCompartidos.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            usuario.FechaRegistro = DateTime.UtcNow;
            usuario.Activo = true;
            return await _repository.CreateAsync(usuario);
        }

        public async Task<Usuario?> UpdateAsync(Usuario usuario)
        {
            return await _repository.UpdateAsync(usuario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<bool> CambiarPasswordAsync(int id, string passwordActual, string passwordNueva)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(passwordActual, usuario.PasswordHash))
                return false;

            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordNueva);
            await _repository.UpdateAsync(usuario);
            return true;
        }
    }
}