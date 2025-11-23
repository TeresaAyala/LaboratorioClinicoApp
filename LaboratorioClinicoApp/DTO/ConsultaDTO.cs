using System.ComponentModel.DataAnnotations;

namespace LaboratorioClinicoApp.DTO
{
    public class ConsultaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "⚠️ El ID de la cita es requerido.")]
        public int IdCita { get; set; }

        [Required(ErrorMessage = "⚠️ El ID del paciente es requerido.")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "⚠️ El ID del doctor es requerido.")]
        public int IdDoctor { get; set; }

        [Required(ErrorMessage = "⚠️ La fecha de la consulta es requerida.")]
        public DateTime FechaConsulta { get; set; }

        [Required(ErrorMessage = "⚠️ El motivo de la consulta es requerido.")]
        [StringLength(200, ErrorMessage = "⚠️ El motivo no puede superar los 200 caracteres.")]
        public string Motivo { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "⚠️ El diagnóstico no puede superar los 300 caracteres.")]
        public string Diagnostico { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "⚠️ El tratamiento no puede superar los 300 caracteres.")]
        public string Tratamiento { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "⚠️ Las observaciones no pueden superar los 300 caracteres.")]
        public string Observaciones { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ El estado de la consulta es requerido.")]
        [StringLength(50, ErrorMessage = "⚠️ El estado no puede superar los 50 caracteres.")]
        public string Estado { get; set; } = "Activa";
    }
}
