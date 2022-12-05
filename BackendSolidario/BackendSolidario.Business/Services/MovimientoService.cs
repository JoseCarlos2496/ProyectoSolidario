using Microsoft.Extensions.Logging;
using BackendSolidario.Business.Interfaces;
using BackendSolidario.Core.Interfaces;
using BackendSolidario.Core.Models;
using BackendSolidario.Core.Exceptions;

namespace BackendSolidario.Business.Services {
    public class MovimientoService : IMovimientoService {

        private CuentaService _cuentaService;
        private ILogger<MovimientoService> _logger;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IBaseRepository<Movimiento, int> _baseRepository;

        public MovimientoService(IMovimientoRepository movimientoRepository, IBaseRepository<Movimiento, int> baseRepository, ICuentaRepository cuentaRepository, ILogger<MovimientoService> logger, ILogger<CuentaService> loggerCuenta) {
            _logger = logger;
            _baseRepository = baseRepository;
            _movimientoRepository = movimientoRepository;
            _cuentaService = new CuentaService(cuentaRepository, loggerCuenta);
        }
        public MovimientoService(IMovimientoRepository movimientoRepository, ILogger<MovimientoService> logger) {
            _logger = logger;
            _movimientoRepository = movimientoRepository;
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosAsync() {

            IEnumerable<Movimiento> movimientos = await _baseRepository.GetAllAsync();

            return movimientos;
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosByNumeroCuentaAsync(string numeroCuenta) {

            IEnumerable<Movimiento> movimientosByNumeroCuenta = await _movimientoRepository.GetMovimientosByNumeroCuentaAsync(numeroCuenta);

            return movimientosByNumeroCuenta;
        }

        public async Task<Movimiento> GetMovimientoByIdAsync(int id) {

            Movimiento movimiento = await _baseRepository.GetByIdAsync(id);

            return movimiento;
        }

        public async Task<Movimiento> AddMovimientoAsync(Movimiento movimiento) {

            var _lstMovimientos = await _movimientoRepository.GetMovimientosByNumeroCuentaAsync(movimiento.CuentaId);
            Movimiento movimientoNuevo;

            if (!_lstMovimientos.Any()) {
                Cuenta _cuenta = await _cuentaService.GetCuentaByNumeroCuentaAsync(movimiento.CuentaId);
                movimiento.Valor = movimiento.TipoMovimiento.Equals("Debito", StringComparison.CurrentCultureIgnoreCase) ? -1 * movimiento.Valor : movimiento.Valor;
                double saldo = movimiento.Valor + _cuenta.SaldoInicial;
                movimiento.Saldo = saldo;
                if (saldo < 0) throw new BusinessException(Constants.NONAVAILABLEBALANCE);
                movimientoNuevo = await _baseRepository.AddAsync(movimiento);
            } else {
                Movimiento _movimiento = _lstMovimientos.Last();
                movimiento.Valor = movimiento.TipoMovimiento.Equals("Debito", StringComparison.CurrentCultureIgnoreCase) ? -1 * movimiento.Valor : movimiento.Valor;
                double saldo = movimiento.Valor + _movimiento.Saldo;
                movimiento.Saldo = saldo;
                if (saldo < 0) throw new BusinessException(Constants.NONAVAILABLEBALANCE);
                movimientoNuevo = await _baseRepository.AddAsync(movimiento);
            }


            return movimientoNuevo;
        }

        public async Task<bool> UpdateMovimientoAsync(Movimiento movimiento) {

            await _baseRepository.GetByIdAsync(movimiento.Id);

            var movimientoActualizado = await _baseRepository.UpdateAsync(movimiento);

            return movimientoActualizado;
        }

        public async Task<bool> DeleteMovimientoAsync(int id) {

            await _baseRepository.GetByIdAsync(id);

            var movimientoEliminado = await _baseRepository.DeleteAsync(id);

            return movimientoEliminado;
        }
    }
}
