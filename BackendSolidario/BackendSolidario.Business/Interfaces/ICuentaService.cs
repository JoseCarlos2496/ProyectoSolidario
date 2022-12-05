using BackendSolidario.Core.Models;

namespace BackendSolidario.Business.Interfaces {
    public interface ICuentaService {
        public Task<IEnumerable<Cuenta>> GetCuentasAsync();
        public Task<IEnumerable<Cuenta>> GetCuentasByClientIdAsync(int clientId);
        public Task<Cuenta> GetCuentaByNumeroCuentaAsync(string numeroCuenta);
        public Task<Cuenta> GetCuentaByIdAsync(int id);
        public Task<Cuenta> AddCuentaAsync(Cuenta cuenta);
        public Task<bool> UpdateCuentaAsync(Cuenta cuenta);
        public Task<bool> DeleteCuentaAsync(int id);
    }
}
