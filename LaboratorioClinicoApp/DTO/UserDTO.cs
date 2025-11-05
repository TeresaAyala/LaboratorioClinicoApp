namespace LaboratorioClinicoApp.DTO
{
    public class Usuario
    {
        public int Id { get; set; } = 0;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int IdRol { get; set; } = 1;
    }
   
}
