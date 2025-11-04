using LaboratorioClinicoApp.DTO;
using LaboratorioClinicoApp.Services;
using System.Net.Http.Headers;

namespace LaboratorioClinicoApp.Components.Services
{
    public class ResultadoService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthServices _authService;

        public ResultadoService(HttpClient httpClient, AuthServices authService)
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

        public async Task<List<ResultadoDTO>> GetResultados()
        {
            await AgregarTokenAsync();
            var response = await _httpClient.GetFromJsonAsync<List<ResultadoDTO>>("/api/resultado");
            return response ?? new List<ResultadoDTO>();
        }

        public async Task<ResultadoDTO?> GetResultadoById(int id)
        {
            await AgregarTokenAsync();
            return await _httpClient.GetFromJsonAsync<ResultadoDTO>($"/api/resultado/{id}");
        }

        public async Task<string> AddResultado(ResultadoDTO resultado)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PostAsJsonAsync("/api/resultado", resultado);

            if (response.IsSuccessStatusCode)
                return "Resultado agregado exitosamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al agregar resultado: {error}");
        }

        public async Task<string> UpdateResultado(int id, ResultadoDTO resultado)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PutAsJsonAsync($"/api/resultado/{id}", resultado);

            if (response.IsSuccessStatusCode)
                return "Resultado actualizado correctamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al actualizar Resultado: {error}");
        }

        // 🔹 Eliminar Resultado (firma, sin código)
        public string DeleteResultado(int id)
        {
            // Código deshabilitado para no borrar registros
            return "Funcionalidad de eliminación deshabilitada.";
        }
    }
}
