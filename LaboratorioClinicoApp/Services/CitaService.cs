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

        // ----------------------------------------------------
        // Obtener todas las citas activas
        // ----------------------------------------------------
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

        // ----------------------------------------------------
        // AGREGAR CITA
        // ----------------------------------------------------
        public async Task<string> AddCita(CitaDTO cita)
        {
            await AgregarTokenAsync();

            if (cita.TipoCita == "EXAMEN")
                cita.IdDoctor = null;

            var response = await _httpClient.PostAsJsonAsync("/api/cita", cita);

            var resultado = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error al agregar cita: {resultado}");

            return resultado;
        }

        // ----------------------------------------------------
        // ACTUALIZAR CITA
        // ----------------------------------------------------
        public async Task<string> UpdateCita(int id, CitaDTO cita)
        {
            await AgregarTokenAsync();

            if (cita.TipoCita == "EXAMEN")
                cita.IdDoctor = null;

            var response = await _httpClient.PutAsJsonAsync($"/api/cita/{id}", cita);

            var resultado = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error al actualizar cita: {resultado}");

            return resultado;
        }

        // ----------------------------------------------------
        // ELIMINAR (deshabilitado)
        // ----------------------------------------------------
        public string DeleteCita(int id)
        {
            return "Funcionalidad de eliminación deshabilitada.";
        }

        // ----------------------------------------------------
        // Horas disponibles
        // ----------------------------------------------------
        public async Task<List<DateTime>> GetHorasDisponiblesAsync(int? idDoctor)
        {
            if (idDoctor == null)
                return new List<DateTime>();

            var response = await _httpClient.GetFromJsonAsync<List<DateTime>>(
                $"/api/cita/horasDisponibles/{idDoctor.Value}"
            );

            return response ?? new List<DateTime>();
        }

        // ----------------------------------------------------
        // Citas por doctor y fecha
        // ----------------------------------------------------
        public async Task<List<CitaDTO>> GetCitasPorDoctorYFecha(int idDoctor, DateTime fecha)
        {
            await AgregarTokenAsync();

            string fechaISO = fecha.ToString("yyyy-MM-dd");

            var response = await _httpClient.GetFromJsonAsync<List<CitaDTO>>(
                $"/api/cita/doctor/{idDoctor}/fecha/{fechaISO}"
            );

            return response ?? new List<CitaDTO>();
        }

        // ----------------------------------------------------
        // 🚀 FALTA: Citas por paciente
        // ----------------------------------------------------
        public async Task<List<CitaDTO>> GetCitasPorPaciente(int idPaciente)
        {
            await AgregarTokenAsync();

            var response = await _httpClient.GetFromJsonAsync<List<CitaDTO>>(
                $"/api/cita/paciente/{idPaciente}"
            );

            return response ?? new List<CitaDTO>();
        }
    }
}
