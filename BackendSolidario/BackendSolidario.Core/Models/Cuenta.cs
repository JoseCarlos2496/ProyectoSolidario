using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSolidario.Core.Models {

    public class Cuenta : BaseEntity<int>{

        public Cuenta() {
            this.Movimientos = new HashSet<Movimiento>();
        }

        //PK
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public double SaldoInicial { get; set; }
        public bool Estado { get; set; }

        //Hereda
        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        //Hijo
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
