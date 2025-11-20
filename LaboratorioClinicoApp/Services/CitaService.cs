using LaboratorioClinicoApp.DTO;
using System.Net.Http.Headers;

namespace LaboratorioClinicoApp.Services
{
    public class CitaService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthServices _authService;

        public CitaService(HttpClient httpClient, AuthServices authService)
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

        public async Task<List<CitaDTO>> GetCitas()
        {
            await AgregarTokenAsync();
            var response = await _httpClient.GetFromJsonAsync<List<CitaDTO>>("/api/cita");
            return response ?? new List<CitaDTO>();
        }

        public async Task<CitaDTO?> GetCitaById(int id)
        {
            await AgregarTokenAsync();
            return await _httpClient.GetFromJsonAsync<CitaDTO>($"/api/cita/{id}");
        }

        public async Task<string> AddCita(CitaDTO cita)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PostAsJsonAsync("/api/cita", cita);

            if (response.IsSuccessStatusCode)
                return "Cita agregada exitosamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al agregar cita: {error}");
        }

        public async Task<string> UpdateCita(int id, CitaDTO cita)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PutAsJsonAsync($"/api/cita/{id}", cita);

            if (response.IsSuccessStatusCode)
                return "Cita actualizada correctamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al actualizar cita: {error}");
        }

        // 🔹 Eliminar Cita (firma, sin código)
        public string DeleteCita(int id)
        {
            // Código deshabilitado para no borrar registros
            return "Funcionalidad de eliminación deshabilitada.";
        }

        public async Task<List<DateTime>> GetHorasDisponiblesAsync(int idDoctor)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.GetFromJsonAsync<List<DateTime>>($"/api/cita/horasDisponibles/{idDoctor}");
            return response ?? new List<DateTime>();
        }

        public async Task<List<CitaDTO>> GetCitasPorDoctorYFecha(int idDoctor, DateTime fecha)
        {
            await AgregarTokenAsync();
            string fechaISO = fecha.ToString("yyyy-MM-dd");
            var response = await _httpClient.GetFromJsonAsync<List<CitaDTO>>($"/api/cita/doctor/{idDoctor}/fecha/{fechaISO}");
            return response ?? new List<CitaDTO>();
        }

    }
}
