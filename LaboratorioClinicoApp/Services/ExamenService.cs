using LaboratorioClinicoApp.DTO;
using LaboratorioClinicoApp.Services;
using System.Net.Http.Headers;

namespace LaboratorioClinicoApp.Components.Services
{
    public class ExamenService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthServices _authService;

        public ExamenService(HttpClient httpClient, AuthServices authService)
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

        public async Task<List<ExamenDTO>> GetExamenes()
        {
            await AgregarTokenAsync();
            var response = await _httpClient.GetFromJsonAsync<List<ExamenDTO>>("/api/examen");
            return response ?? new List<ExamenDTO>();
        }

        public async Task<ExamenDTO?> GetExamenById(int id)
        {
            await AgregarTokenAsync();
            return await _httpClient.GetFromJsonAsync<ExamenDTO>($"/api/examen/{id}");
        }

        public async Task<string> AddExamen(ExamenDTO examen)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PostAsJsonAsync("/api/examen", examen);

            if (response.IsSuccessStatusCode)
                return "Examen agregado exitosamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al agregar examen: {error}");
        }

        public async Task<string> UpdateExamen(int id, ExamenDTO examen)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PutAsJsonAsync($"/api/examen/{id}", examen);

            if (response.IsSuccessStatusCode)
                return "Examen actualizado correctamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al actualizar examen: {error}");
        }

        // 🔹 Eliminar Examen (firma, sin código)
        public string DeleteExamen(int id)
        {
            // Código deshabilitado para no borrar registros
            return "Funcionalidad de eliminación deshabilitada.";
        }

    }
}
