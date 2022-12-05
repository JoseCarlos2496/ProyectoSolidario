using BackendSolidario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BackendSolidario.Core.Models;
using BackendSolidario.Core.Interfaces;

namespace BackendSolidario.Infrastructure.Repositories {
    public class MovimientoRepository : IMovimientoRepository {
        private readonly BcoSolidarioDbContext _db;

        public MovimientoRepository (BcoSolidarioDbContext db) {
            _db = db;
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosByNumeroCuentaAsync(string numeroCuenta) {
            var listOfMovimientos = await _db.Movimientos.ToListAsync();
            var movimientos = listOfMovimientos.Where(x => x.CuentaId == numeroCuenta).ToList();
            return movimientos;
        }
    }
}
