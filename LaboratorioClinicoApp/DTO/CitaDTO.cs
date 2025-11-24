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

        // ✔ NUEVO: Tipo de cita (CONSULTA | EXAMEN)
        [Required(ErrorMessage = "⚠️ El tipo de cita es requerido.")]
        public string TipoCita { get; set; } = "EXAMEN";

        // ✔ Cambiado: Estado debe coincidir con API (Activo / Inactivo / Cancelado)
        public string Estado { get; set; } = "Activo";

        public string? NotasConsulta { get; set; }

        [Required(ErrorMessage = "⚠️ El ID del paciente es requerido.")]
        public int IdPaciente { get; set; }

        // ✔ IdDoctor ahora es OPCIONAL (nullable)
        public int? IdDoctor { get; set; }

        // ✔ Información opcional enviada por la API
        public string? PacienteNombre { get; set; }
        public string? DoctorNombre { get; set; }
    }
}
