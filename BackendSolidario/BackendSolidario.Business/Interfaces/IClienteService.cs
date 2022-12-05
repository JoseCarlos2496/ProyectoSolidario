using BackendSolidario.Core.Models;

namespace BackendSolidario.Business.Interfaces {
    public interface IClienteService {
        Task<IEnumerable<Cliente>> GetClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<Cliente> GetClienteByIdentificacionAsync(string id);
        Task<Cliente> GetClienteByUsername(string username);
        Task<Cliente> AddClienteAsync(Cliente cliente);
        Task<bool> UpdateClienteAsync(Cliente cliente);
        Task<bool> DeleteClienteAsync(int id);
    }
}
