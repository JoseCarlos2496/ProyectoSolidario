namespace BackendSolidario.Core.Models {
    public class ReporteMovimiento {

        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public double SaldoDisponible { get; set; }
        public double SaldoMensual { get; set; }
    }
}
