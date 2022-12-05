using BackendSolidario.Core.Models;

namespace BackendSolidario.Core.Interfaces {
    public interface ICuentaRepository {
        public Task<Cuenta> GetCuentaByNumeroCuentaAsync(string numeroCuenta);
        public Task<IEnumerable<Cuenta>> GetCuentasByClientIdAsync(int clientId);

    }
}
