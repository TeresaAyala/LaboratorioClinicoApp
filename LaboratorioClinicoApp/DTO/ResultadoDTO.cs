namespace LaboratorioClinicoApp.DTO
{
    public class ResultadoDTO
    {
        public int IdResultado { get; set; }
        public string Detalle { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public bool Estado { get; set; }
        public int IdExamen { get; set; }
        public int IdDoctor { get; set; }

    }
}