namespace LaboratorioClinicoApp.DTO
{
    public class DoctorDTO
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string LicenciaMedica { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public int IdUsuario { get; set; } // Para mantener consistencia con la entidad

    }
}
