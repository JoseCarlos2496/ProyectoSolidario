using BackendSolidario.Core.Models;

namespace BackendSolidario.Business.Interfaces {
    public interface IReporteService {
        public Task<IEnumerable<ReporteMovimiento>> GetReporteCuenta(string identificacion);
    }
}
