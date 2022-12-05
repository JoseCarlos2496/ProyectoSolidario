using BackendSolidario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BackendSolidario.Core.Interfaces;
using BackendSolidario.Core.Models;
using BackendSolidario.Infrastructure.Exceptions;

namespace BackendSolidario.Infrastructure.Repositories {
    public class ClienteRepository : IClienteRepository {
        private readonly BcoSolidarioDbContext _db;

        public ClienteRepository(BcoSolidarioDbContext db) {
            _db = db;
        }

        public async Task<Cliente> GetByUsername(string username) {
            var usuario = await _db.Clientes.Where(x => x.Usuario == username).FirstOrDefaultAsync();
            if (usuario == null) throw new NotFoundException(Constants.CLIENTNOTEXISTS);

            return usuario;
        }

        
        public async Task<Cliente> GetClienteByIdentificacionAsync(string identificacion) {
            var usuario = await _db.Clientes.Where(x => x.Identificacion == identificacion && x.Estado == true).FirstOrDefaultAsync();
            if (usuario == null) throw new NotFoundException(Constants.CLIENTNOTEXISTS);

            return usuario;
        }
        
    }
}
