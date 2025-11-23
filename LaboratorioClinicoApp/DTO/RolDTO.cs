using System.ComponentModel.DataAnnotations;

namespace LaboratorioClinicoApp.DTO
{
    public class RolDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "⚠️ El nombre del rol es requerido.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ La descripción del rol es requerida.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "⚠️ El estado es requerido.")]
        public string Estado { get; set; } = "Activo";
    }
}
