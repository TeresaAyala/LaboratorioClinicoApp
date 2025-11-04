using LaboratorioClinicoApp.DTO;
using System.Net.Http.Headers;

namespace LaboratorioClinicoApp.Services
{
    public class RolService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthServices _authService;

        public RolService(HttpClient httpClient, AuthServices authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        private async Task AgregarTokenAsync()
        {
            var token = await _authService.GetToken();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("El token es nulo o inválido. Iniciar sesión.");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<RolDTO>> GetRoles()
        {
            await AgregarTokenAsync();
            var response = await _httpClient.GetFromJsonAsync<List<RolDTO>>("/api/rol");
            return response ?? new List<RolDTO>();
        }

        public async Task<RolDTO?> GetRolById(int id)
        {
            await AgregarTokenAsync();
            return await _httpClient.GetFromJsonAsync<RolDTO>($"/api/rol/{id}");
        }

        public async Task<string> AddRol(RolDTO rol)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PostAsJsonAsync("/api/rol", rol);

            if (response.IsSuccessStatusCode)
                return "Rol agregado exitosamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al agregar rol: {error}");
        }

        public async Task<string> UpdateRol(int id, RolDTO rol)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PutAsJsonAsync($"/api/rol/{id}", rol);

            if (response.IsSuccessStatusCode)
                return "Rol actualizado correctamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al actualizar Rol: {error}");
        }

        // 🔹 Eliminar Rol (firma, sin código)
        public string DeleteRol(int id)
        {
            // Código deshabilitado para no borrar registros
            return "Funcionalidad de eliminación deshabilitada.";
        }
    }
}

