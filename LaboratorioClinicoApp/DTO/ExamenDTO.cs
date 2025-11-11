namespace LaboratorioClinicoApp.DTO
{
    public class ExamenDTO

    {
        public int Id { get; set; }
        public string TipoExamen { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public int IdCita { get; set; }
        public int IdPaciente { get; set; }

    }

}

