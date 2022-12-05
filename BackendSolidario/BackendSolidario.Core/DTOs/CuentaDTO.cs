namespace BackendSolidario.Core.DTOs {
    public class CuentaDto {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public string? TipoCuenta { get; set; } = "Ahorro";
        public double SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int ClienteId { get; set; }
    }
}
