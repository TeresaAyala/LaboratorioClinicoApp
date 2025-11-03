using LaboratorioClinicoApp.DTO;
using System.Net.Http.Headers;

namespace LaboratorioClinicoApp.Services
{
    public class PacienteService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthServices _authService;

        public PacienteService(HttpClient httpClient, AuthServices authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        // 🔹 Agregar token en cada solicitud
        private async Task AgregarTokenAsync()
        {
            var token = await _authService.GetToken();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("El token es nulo o inválido. Iniciar sesión.");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        // 🔹 Obtener todos los pacientes
        public async Task<List<PacienteDTO>> GetPacientes()
        {
            await AgregarTokenAsync();
            var response = await _httpClient.GetFromJsonAsync<List<PacienteDTO>>("/api/paciente");
            return response ?? new List<PacienteDTO>();
        }

        // 🔹 Obtener un paciente por su ID
        public async Task<PacienteDTO?> GetPacienteById(int id)
        {
            await AgregarTokenAsync();
            return await _httpClient.GetFromJsonAsync<PacienteDTO>($"/api/paciente/{id}");
        }

        // 🔹 Crear un nuevo paciente
        public async Task<string> AddPaciente(PacienteDTO paciente)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PostAsJsonAsync("/api/paciente", paciente);

            if (response.IsSuccessStatusCode)
                return "Paciente agregado exitosamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al agregar paciente: {error}");
        }

        // 🔹 Actualizar un paciente existente
        public async Task<string> UpdatePaciente(int id, PacienteDTO paciente)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PutAsJsonAsync($"/api/paciente/{id}", paciente);

            if (response.IsSuccessStatusCode)
                return "Paciente actualizado correctamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al actualizar paciente: {error}");
        }

        // 🔹 Eliminar Paciente (firma, sin código)
        public string DeletePaciente(int id)
        {
            // Código deshabilitado para no borrar registros
            return "Funcionalidad de eliminación deshabilitada.";
        }
    }
}
