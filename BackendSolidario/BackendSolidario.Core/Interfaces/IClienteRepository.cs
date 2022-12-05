using BackendSolidario.Core.Models;

namespace BackendSolidario.Core.Interfaces {
    public interface IClienteRepository {
        Task<Cliente> GetByUsername(string username);
        Task<Cliente> GetClienteByIdentificacionAsync(string identificacion);

        }
    }
