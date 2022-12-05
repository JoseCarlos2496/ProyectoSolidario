using BackendSolidario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BackendSolidario.Core.Models;
using BackendSolidario.Core.Interfaces;
using BackendSolidario.Infrastructure.Exceptions;

namespace BackendSolidario.Infrastructure.Repositories {
    public class CuentaRepository : ICuentaRepository {
        private readonly BcoSolidarioDbContext _db;

        public CuentaRepository(BcoSolidarioDbContext db) {
            _db = db;
        }

        public async Task<Cuenta> GetCuentaByNumeroCuentaAsync(string numeroCuenta) {
            var cuenta = await _db.Cuentas.Where(x => x.NumeroCuenta == numeroCuenta && x.Estado == true).FirstOrDefaultAsync();
            return cuenta;
        }

        public async Task<IEnumerable<Cuenta>> GetCuentasByClientIdAsync(int clientId) {
            IEnumerable<Cuenta>? cuentas = await _db.Cuentas.Where(x => x.ClienteId == clientId && x.Estado == true).OrderBy(x => x.Id).ToListAsync();
            return cuentas;
        }
    }
}
