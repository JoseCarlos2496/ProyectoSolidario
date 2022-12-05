using BackendSolidario.Core.Models;

namespace BackendSolidario.Business.Interfaces {
    public interface IMovimientoService {
        public Task<IEnumerable<Movimiento>> GetMovimientosAsync();
        public Task<IEnumerable<Movimiento>> GetMovimientosByNumeroCuentaAsync(string numeroCuenta);
        public Task<Movimiento> GetMovimientoByIdAsync(int id);
        public Task<Movimiento> AddMovimientoAsync(Movimiento movimiento);
        public Task<bool> UpdateMovimientoAsync(Movimiento movimiento);
        public Task<bool> DeleteMovimientoAsync(int id);
    }
}
