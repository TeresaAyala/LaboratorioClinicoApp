using System.ComponentModel.DataAnnotations;

namespace LaboratorioClinicoApp.DTO
{
    public class DoctorDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "⚠️ El nombre es requerido.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ El apellido es requerido.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ La especialidad es requerida.")]
        public string Especialidad { get; set; } = string.Empty;

        public string? Telefono { get; set; }
        public string? Email { get; set; }

        [Required(ErrorMessage = "⚠️ La licencia médica es requerida.")]
        public string LicenciaMedica { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ El estado es requerido.")]
        public string Estado { get; set; } = "Activo";

        [Required(ErrorMessage = "⚠️ El ID del usuario es requerido.")]
        public int IdUsuario { get; set; }
    }
}
