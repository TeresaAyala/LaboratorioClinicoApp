using System;
using System.ComponentModel.DataAnnotations;

namespace LaboratorioClinicoApp.DTO
{
    public class ExamenDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "⚠️ El tipo de examen es requerido.")]
        public string TipoExamen { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ La fecha es requerida.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "⚠️ La descripción es requerida.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ El estado es requerido.")]
        public string Estado { get; set; } = "Pendiente";

        [Required(ErrorMessage = "⚠️ El ID de la cita es requerido.")]
        public int IdCita { get; set; }

        [Required(ErrorMessage = "⚠️ El ID del paciente es requerido.")]
        public int IdPaciente { get; set; }

        // ✅ Agregados para coincidir con lo que la API puede enviar
        public string? PacienteNombre { get; set; }
        public DateTime? CitaFecha { get; set; }

        // ✅ Para evitar errores de deserialización si vienen objetos
        public PacienteDTO? Paciente { get; set; }
        public CitaDTO? Cita { get; set; }
    }

}
