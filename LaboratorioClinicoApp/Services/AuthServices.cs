using LaboratorioClinicoApp.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LaboratorioClinicoApp.Services
{
    public class AuthServices
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navManager;
        private string _token;

        public AuthServices(ProtectedLocalStorage localStorage, HttpClient httpClient, NavigationManager navManager)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _navManager = navManager;
        }

        // ================================
        // LOGIN
        // ================================
        public class LoginRequest
        {
            public int Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public int IdRol { get; set; }
        }

        public class LoginResponse
        {
            public string Token { get; set; } = string.Empty;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var request = new LoginRequest
            {
                Username = username,
                Password = password,
                PasswordHash = password
            };

            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (!string.IsNullOrEmpty(result?.Token))
            {
                await _localStorage.SetAsync("authToken", result.Token);
                _token = result.Token;

                _navManager.NavigateTo("/doctores", true);
                return result.Token;
            }

            return null;
        }

        // ================================
        // REGISTRO
        // ================================
        public async Task<(bool ok, string message)> RegisterAsync(string username, string password, int idRol)
        {
            var request = new LoginRequest
            {
                Username = username,
                Password = password,
                PasswordHash = password,
                IdRol = idRol
            };

            var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);
            var resultado = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return (true, "Usuario registrado correctamente.");

            return (false, resultado);
        }

        // ================================
        // ROLES DESDE API (SIN TOKEN)
        // ================================
        public async Task<List<RolDTO>?> GetRolesFromApiAsync()
        {
            try
            {
                // Llamada directa a la API sin agregar Authorization
                return await _httpClient.GetFromJsonAsync<List<RolDTO>>("api/rol");
            }
            catch
            {
                return null;
            }
        }


        // ================================
        // TOKEN
        // ================================
        public async Task<string?> GetToken()
        {
            if (!string.IsNullOrEmpty(_token))
                return _token;

            var result = await _localStorage.GetAsync<string>("authToken");

            if (result.Success)
            {
                _token = result.Value;
                return _token;
            }

            return null;
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await GetToken();
            return !string.IsNullOrEmpty(token) && !IsTokenExpired(token);
        }

        public bool IsTokenExpired(string token)
        {
            try
            {
                var jwt = new JwtSecurityToken(token);
                return jwt.ValidTo < DateTime.UtcNow;
            }
            catch
            {
                return true;
            }
        }

        // ================================
        // LOGOUT
        // ================================
        public async Task Logout()
        {
            _token = null;
            await _localStorage.DeleteAsync("authToken");
            _navManager.NavigateTo("/", true);
        }
    }
}



