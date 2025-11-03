using LaboratorioClinicoApp.DTO;
using System.Net.Http.Headers;

namespace LaboratorioClinicoApp.Services
{
    public class DoctorService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthServices _authService;

        public DoctorService(HttpClient httpClient, AuthServices authService)
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

        // 🔹 Obtener todos los doctores
        public async Task<List<DoctorDTO>> GetDoctors()
        {
            await AgregarTokenAsync();
            var response = await _httpClient.GetFromJsonAsync<List<DoctorDTO>>("/api/doctor");
            return response ?? new List<DoctorDTO>();
        }

        // 🔹 Obtener un doctor por su ID
        public async Task<DoctorDTO?> GetDoctorById(int id)
        {
            await AgregarTokenAsync();
            return await _httpClient.GetFromJsonAsync<DoctorDTO>($"/api/doctor/{id}");
        }

        // 🔹 Crear un nuevo doctor
        public async Task<string> AddDoctor(DoctorDTO doctor)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PostAsJsonAsync("/api/doctor", doctor);

            if (response.IsSuccessStatusCode)
                return "Doctor agregado exitosamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al agregar doctor: {error}");
        }

        // 🔹 Actualizar un doctor existente
        public async Task<string> UpdateDoctor(int id, DoctorDTO doctor)
        {
            await AgregarTokenAsync();
            var response = await _httpClient.PutAsJsonAsync($"/api/doctor/{id}", doctor);

            if (response.IsSuccessStatusCode)
                return "Doctor actualizado correctamente.";

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al actualizar doctor: {error}");
        }

        // 🔹 Eliminar doctor (firma, sin código)
        public string DeleteDoctor(int id)
        {
            // Código deshabilitado para no borrar registros
            return "Funcionalidad de eliminación deshabilitada.";
        }

    }
}

