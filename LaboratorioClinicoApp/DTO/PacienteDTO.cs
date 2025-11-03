namespace LaboratorioClinicoApp.DTO
{
    public class PacienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public bool Estado { get; set; }
        public int IdUsuario { get; set; }
        public int IdDoctor { get; set; }
    }
}
