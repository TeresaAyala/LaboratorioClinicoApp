using LaboratorioClinicoApp.DTO;
using System.Net.Http.Headers;

namespace LaboratorioClinicoApp.Services
{
    public class ConsultaService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthServices _authService;

        public ConsultaService(HttpClient httpClient, AuthServices authService)
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

        public async Task<List<ConsultaDTO>> GetConsultas()
        {
            await AgregarTokenAsync();
            var response = await _httpClient.GetFromJsonAsync<List<ConsultaDTO>>("/api/consulta");
            return response ?? new List<ConsultaDTO>();
        }

        public async Task<ConsultaDTO?> GetConsultaById(int id)
        {
            await AgregarTokenAsync();
            return await _httpClient.GetFromJsonAsync<ConsultaDTO>($"/api/consulta/{id}");
        }

        public async Task<string> AddConsulta(ConsultaDTO consulta)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PostAsJsonAsync("/api/consulta", consulta);

            if (response.IsSuccessStatusCode)
                return "Consulta agregada exitosamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al agregar consulta: {error}");
        }

        public async Task<string> UpdateConsulta(int id, ConsultaDTO consulta)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PutAsJsonAsync($"/api/consulta/{id}", consulta);

            if (response.IsSuccessStatusCode)
                return "Consulta actualizada correctamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al actualizar consulta: {error}");
        }

        // 🔹 Eliminación deshabilitada (igual que Examen)
        public string DeleteConsulta(int id)
        {
            return "Funcionalidad de eliminación deshabilitada.";
        }
    }
}
