using BackendSolidario.Business.Interfaces;
using BackendSolidario.Core.Interfaces;
using BackendSolidario.Core.Models;
using Microsoft.Extensions.Logging;
using BackendSolidario.Infrastructure.Exceptions;

namespace BackendSolidario.Business.Services
{
    public class ReporteService : IReporteService
    {
        private ILogger<ReporteService> _logger;
        private readonly ClienteService _clienteService;
        private readonly CuentaService _cuentaService;
        private readonly MovimientoService _movimientoService;


        public ReporteService(IClienteRepository clienteRepository, ICuentaRepository cuentaRepository,
            IMovimientoRepository movimientoRepository,
            ILogger<ReporteService> logger, ILogger<CuentaService> loggerCuenta,
            ILogger<MovimientoService> loggerMovimiento)
        {
            _logger = logger;
            _clienteService = new ClienteService(clienteRepository);
            _cuentaService = new CuentaService(cuentaRepository, loggerCuenta);
            _movimientoService = new MovimientoService(movimientoRepository, loggerMovimiento);
        }

        public async Task<IEnumerable<ReporteMovimiento>> GetReporteCuenta(string identificacion)
        {
            var saldo = 0.0;
            List<ReporteMovimiento> list = new();

            var cliente = await _clienteService.GetClienteByIdentificacionAsync(identificacion);
            var cuentaByClientId = await _cuentaService.GetCuentasByClientIdAsync(cliente.Id);
            var cuentas = cuentaByClientId.ToList();


            if (cuentas.Any()) {
                foreach (var cuenta in cuentas) {
                    var movimientosByNumeroCuenta =
                        await _movimientoService.GetMovimientosByNumeroCuentaAsync(cuenta.NumeroCuenta);

                    var movimientos = movimientosByNumeroCuenta.ToList();
                    if (movimientos.Any()) {
                        foreach (var movimiento in movimientos) {
                            saldo = movimiento.Saldo; //Se toma el ultimo saldo registrado en base
                        }

                        ReporteMovimiento reporte = new()
                        {
                            Identificacion = cliente.Identificacion,
                            Nombre = cliente.Nombre,
                            NumeroCuenta = cuenta.NumeroCuenta,
                            TipoCuenta = cuenta.TipoCuenta,
                            SaldoDisponible = saldo,
                            SaldoMensual = saldo / 12
                        };

                        list.Add(reporte);
                    }
                    else {
                        ReporteMovimiento reporte = new()
                        {
                            Identificacion = cliente.Identificacion,
                            Nombre = cliente.Nombre,
                            NumeroCuenta = cuenta.NumeroCuenta,
                            TipoCuenta = cuenta.TipoCuenta,
                            SaldoDisponible = cuenta.SaldoInicial,
                            SaldoMensual = cuenta.SaldoInicial / 12
                        };
                        list.Add(reporte);
                    }
                }
            }
            else {
                throw new NotFoundException(Constants.NONACCOUNT);
            }


            IEnumerable<ReporteMovimiento> reporteMovimientos = list.ToList();
            return reporteMovimientos;
        }
    }
}