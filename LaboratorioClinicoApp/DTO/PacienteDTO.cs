using System;
using System.ComponentModel.DataAnnotations;

namespace LaboratorioClinicoApp.DTO
{
    public class PacienteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "⚠️ El nombre es requerido.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ El apellido es requerido.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ La fecha de nacimiento es requerida.")]
        public DateTime FechaNacimiento { get; set; }

        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "⚠️ El estado es requerido.")]
        public string Estado { get; set; } = "Pendiente";

        [Required(ErrorMessage = "⚠️ El ID del usuario es requerido.")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "⚠️ El ID del doctor es requerido.")]
        public int IdDoctor { get; set; }
    }
}
