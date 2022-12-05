namespace BackendSolidario.Core.Models {

    public class Cliente : BaseEntity<int> {

        public Cliente() {
            Cuentas = new HashSet<Cuenta>();
        }

        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }//Usuario
        public string Contrasena { get; set; }
        public string? Salt { get; set; }
        public bool Estado { get; set; } = true;

        //Hijo
        public virtual ICollection<Cuenta> Cuentas { get; set; }
    }
}