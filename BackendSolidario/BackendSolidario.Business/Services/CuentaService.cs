using BackendSolidario.Business.Interfaces;
using BackendSolidario.Core.Exceptions;
using BackendSolidario.Core.Interfaces;
using BackendSolidario.Core.Models;
using BackendSolidario.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace BackendSolidario.Business.Services {
    public class CuentaService : ICuentaService {

        private ILogger<CuentaService> _logger;
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IBaseRepository<Cuenta, int> _repository;
        private readonly IBaseRepository<Cliente, int> _clienteRpository;
        private readonly IBaseRepository<Movimiento, int> _movimientoRpository;

        public CuentaService(ICuentaRepository cuentaRepository, IBaseRepository<Cuenta, int> repository,
            IBaseRepository<Cliente, int> clienteRpository, IBaseRepository<Movimiento, int> movimientoRpository,
            ILogger<CuentaService> logger) {
            _logger = logger;
            _repository = repository;
            _clienteRpository = clienteRpository;
            _movimientoRpository = movimientoRpository;
            _cuentaRepository = cuentaRepository;
        }

        public CuentaService(ICuentaRepository cuentaRepository, ILogger<CuentaService> logger) {
            _logger = logger;
            _cuentaRepository = cuentaRepository;
        }

        public async Task<IEnumerable<Cuenta>> GetCuentasAsync() {

            IEnumerable<Cuenta> cuenta = await _repository.GetAllAsync();

            return cuenta;
        }

        public async Task<IEnumerable<Cuenta>> GetCuentasByClientIdAsync(int clientId) {

            IEnumerable<Cuenta> cuenta = await _cuentaRepository.GetCuentasByClientIdAsync(clientId);

            return cuenta;
        }

        public async Task<Cuenta> GetCuentaByNumeroCuentaAsync(string numeroCuenta) {

            Cuenta cuenta = await _cuentaRepository.GetCuentaByNumeroCuentaAsync(numeroCuenta);

            return cuenta;
        }

        public async Task<Cuenta> GetCuentaByIdAsync(int id) {

            Cuenta cuenta = await _repository.GetByIdAsync(id);
            if (!cuenta.Estado) throw new BusinessException(Constants.NOTFOUND);

            return cuenta;
        }

        public async Task<Cuenta> AddCuentaAsync(Cuenta cuenta) {
            var cliente = await _clienteRpository.GetByIdAsync(cuenta.ClienteId);
            if (cliente == null) throw new NotFoundException(Constants.NOTFOUND);
            
            var _cuenta = await _cuentaRepository.GetCuentaByNumeroCuentaAsync(cuenta.NumeroCuenta);
            if (_cuenta != null) throw new BusinessException(Constants.ACCOUNTEXISTS);

            if (cuenta.SaldoInicial < 100) throw new BusinessException(Constants.MINIMUNDEPOSIT);
            cuenta.SaldoInicial *= 1.03;//Valor del interes
            var cuentaNuevo = await _repository.AddAsync(cuenta);

            Movimiento movimientoNuevo = new() {
                Fecha = DateTime.Now,
                TipoMovimiento = "Depósito",
                Valor = cuenta.SaldoInicial,
                CuentaId = cuenta.Id.ToString(),
                Cuenta = cuenta,
                Saldo = cuenta.SaldoInicial
            };
            var _ = await _movimientoRpository.AddAsync(movimientoNuevo);

            return cuentaNuevo;
        }

        public async Task<bool> UpdateCuentaAsync(Cuenta cuenta) {

            var _cuenta = await _cuentaRepository.GetCuentaByNumeroCuentaAsync(cuenta.NumeroCuenta);
            if (!_cuenta.Estado) throw new BusinessException(Constants.NOTFOUND);

            cuenta.Id = _cuenta.Id;
            var cuentaActualizado = await _repository.UpdateAsync(cuenta);

            return cuentaActualizado;
        }

        public async Task<bool> DeleteCuentaAsync(int id) {

            Cuenta _cuenta = await _repository.GetByIdAsync(id);
            if (!_cuenta.Estado) throw new BusinessException(Constants.NOTFOUND);
            _cuenta.Estado = false;

            var cuentaEliminado = await _repository.UpdateAsync(_cuenta);

            return cuentaEliminado;
        }

    }
}
