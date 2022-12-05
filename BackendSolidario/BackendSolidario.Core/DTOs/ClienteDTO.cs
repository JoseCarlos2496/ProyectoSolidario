namespace BackendSolidario.Core.DTOs {
    public class ClienteDto {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; } //Usuario
        public string Contrasena { get; set; }
        public string? Salt { get; set; }
        public bool? Estado { get; set; } = true;

    }
}