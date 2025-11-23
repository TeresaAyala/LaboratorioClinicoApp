using System;
using System.ComponentModel.DataAnnotations;

namespace LaboratorioClinicoApp.DTO
{
    public class ResultadoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "⚠️ El detalle del resultado es requerido.")]
        [StringLength(500, ErrorMessage = "⚠️ El detalle no puede superar los 500 caracteres.")]
        public string Detalle { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ La fecha de emisión es requerida.")]
        public DateTime FechaEmision { get; set; }

        [Required(ErrorMessage = "⚠️ El ID del paciente es requerido.")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "⚠️ El ID del examen es requerido.")]
        public int IdExamen { get; set; }

        [Required(ErrorMessage = "⚠️ El ID del doctor es requerido.")]
        public int IdDoctor { get; set; }

        [Required(ErrorMessage = "⚠️ El estado del resultado es requerido.")]
        [StringLength(50, ErrorMessage = "⚠️ El estado no puede superar los 50 caracteres.")]
        public string Estado { get; set; } = Validado;

        // ✅ Estados permitidos (coinciden con la entidad)
        public const string Validado = "Validado";
        public const string Entregado = "Entregado";
        public const string Anulado = "Anulado";

        // ✅ (Opcional) Para mostrar en tablas sin romper compilación
        public PacienteDTO? Paciente { get; set; }
        public ExamenDTO? Examen { get; set; }
        public DoctorDTO? Doctor { get; set; }
    }
}
