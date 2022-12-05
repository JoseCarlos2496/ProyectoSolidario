using Microsoft.Extensions.Logging;
using BackendSolidario.Business.Helper;
using BackendSolidario.Business.Interfaces;
using BackendSolidario.Core.Interfaces;
using BackendSolidario.Core.Models;
using BackendSolidario.Core.Exceptions;

namespace BackendSolidario.Business.Services {
    public class ClienteService : IClienteService {

        private readonly CuentaService _cuentaService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IBaseRepository<Cliente, int> _baseRepository;

        public ClienteService(IBaseRepository<Cliente, int> baseRepository, ICuentaRepository cuentaRepository, 
            ILogger<CuentaService> loggerCuenta, IClienteRepository clienteRepository) {
            _baseRepository = baseRepository;
            _clienteRepository = clienteRepository;
            _cuentaService = new CuentaService(cuentaRepository, loggerCuenta);
        }

        public ClienteService(IClienteRepository clienteRepository) {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync() {

            IEnumerable<Cliente> clientes = await _baseRepository.GetAllAsync();
            return clientes;
        }


        public async Task<Cliente> GetClienteByIdAsync(int id) {
            Cliente cliente = await _baseRepository.GetByIdAsync(id);
            return cliente;
        }


        public async Task<Cliente> GetClienteByIdentificacionAsync(string identificacion) {
            Cliente cliente = await _clienteRepository.GetClienteByIdentificacionAsync(identificacion);
            return cliente;
        }


        public async Task<Cliente> GetClienteByUsername(string username) {
            Cliente cliente = await _clienteRepository.GetByUsername(username);
            return cliente;
        }

        public async Task<Cliente> AddClienteAsync(Cliente cliente) {

            HashedPassword hashedPassword;
            var lst = await _baseRepository.GetAllAsync();

            foreach (var item in lst) {
                if (item.Usuario.Equals(cliente.Usuario, StringComparison.InvariantCultureIgnoreCase) && item.Estado == true) {

                    throw new BusinessException(Constants.CLIENTEXISTS);

                } else if (item.Usuario.Equals(cliente.Usuario, StringComparison.InvariantCultureIgnoreCase) && item.Estado == false) {

                    hashedPassword = HashHelper.Hash(cliente.Contrasena);
                    cliente.Contrasena = hashedPassword.Password;
                    cliente.Salt = hashedPassword.Salt;

                    await _baseRepository.UpdateAsync(cliente);
                    return cliente;

                }
            }

            hashedPassword = HashHelper.Hash(cliente.Contrasena);
            cliente.Contrasena = hashedPassword.Password;
            cliente.Salt = hashedPassword.Salt;

            var clienteNuevo = await _baseRepository.AddAsync(cliente);

            return clienteNuevo;
        }

        public async Task<bool> UpdateClienteAsync(Cliente cliente) {

            Cliente _cliente = await _baseRepository.GetByIdAsync(cliente.Id);

            if (!_cliente.Estado) throw new BusinessException(Constants.NOTFOUND);

            cliente.Contrasena = _cliente.Contrasena;
            cliente.Salt = _cliente.Salt;

            var clienteActualizado = await _baseRepository.UpdateAsync(cliente);

            return clienteActualizado;
        }

        public async Task<bool> DeleteClienteAsync(int id) {

            Cliente _cliente = await _baseRepository.GetByIdAsync(id);

            if (!_cliente.Estado) {
                throw new BusinessException(Constants.NOTFOUND);
            } else {
                IEnumerable<Cuenta> _cuentas = await _cuentaService.GetCuentasByClientIdAsync(id);
                if (_cuentas.Any()) {
                    foreach (var cuenta in _cuentas) {
                        await _cuentaService.DeleteCuentaAsync(cuenta.Id);
                    }
                }
            }

            _cliente.Estado = false;

            var cuentaEliminado = await _baseRepository.UpdateAsync(_cliente);

            return cuentaEliminado;
        }
    }
}
