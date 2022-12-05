using BackendSolidario.Core.Models;

namespace BackendSolidario.Core.Interfaces {
    public interface IMovimientoRepository {
        public Task<IEnumerable<Movimiento>> GetMovimientosByNumeroCuentaAsync(string numeroCuenta);
    }
}
