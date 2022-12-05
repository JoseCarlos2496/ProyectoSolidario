using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSolidario.Core.Models {
    public class Movimiento : BaseEntity<int> {
        public DateTime Fecha { get; set; }
        public string? TipoMovimiento { get; set; } = "Ahorro";
        public double Valor { get; set; }
        public double Saldo { get; set; }

        //Hereda
        [ForeignKey("CuentaId")]
        public string CuentaId { get; set; }
        public virtual Cuenta Cuenta { get; set; }
    }
}
