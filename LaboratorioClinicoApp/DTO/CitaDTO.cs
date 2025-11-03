namespace LaboratorioClinicoApp.DTO
{
    public class CitaDTO
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public string? NotasConsulta { get; set; }
        public int IdPaciente { get; set; }
        public int IdDoctor { get; set; }
    }
}
