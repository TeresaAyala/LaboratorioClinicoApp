using System;
using System.ComponentModel.DataAnnotations;

namespace LaboratorioClinicoApp.DTO
{
    public class CitaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "⚠️ La fecha y hora de la cita es requerida.")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "⚠️ El motivo de la cita es requerido.")]
        public string Motivo { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ El estado de la cita es requerido.")]
        public string Estado { get; set; } = "Pendiente";

        public string? NotasConsulta { get; set; }

        [Required(ErrorMessage = "⚠️ El ID del paciente es requerido.")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "⚠️ El ID del doctor es requerido.")]
        public int IdDoctor { get; set; }

        // ✅ Campos opcionales para evitar errores si la API envía datos adicionales
        public string? PacienteNombre { get; set; }
        public string? DoctorNombre { get; set; }
    }
}
