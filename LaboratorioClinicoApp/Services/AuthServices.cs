using LaboratorioClinicoApp.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;

namespace LaboratorioClinicoApp.Services
{
    public class AuthServices
    {
        private readonly ProtectedLocalStorage _LocalStorage;
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navManager;
        private string _token;

        // ✅ Constructor CORREGIDO: ahora recibe también NavigationManager
        public AuthServices(ProtectedLocalStorage localStorage, HttpClient httpClient, NavigationManager navManager)
        {
            _LocalStorage = localStorage;
            _httpClient = httpClient;
            _navManager = navManager;
        }

        // Clases internas
        public class LoginRequest
        {
            public int Id { get; set; } = 0;
            public string Username { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public int IdRol { get; set; } = 0;
        }

        public class LoginResponse
        {
            public string Token { get; set; } = string.Empty;
        }

        // ✅ Login: guarda token y redirige al menú
        public async Task<string?> LoginAsync(string username, string password)
        {
            var request = new LoginRequest
            {
                Username = username,
                Password = password,
                PasswordHash = password
            };

            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
            var resultado = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (result?.Token != null)
                {
                    // ✅ Guardamos el token en almacenamiento local
                    await _LocalStorage.SetAsync("authToken", result.Token);

                    // ✅ Redirigimos al menú principal (por ejemplo, doctores)
                    _navManager.NavigateTo("/doctores", forceLoad: true);

                    return result.Token;
                }
            }

            return null;
        }

        // ---- REGISTRO ----
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
            {
                // Sin redirigir aquí: el componente manejará la navegación
                return (true, "Usuario registrado correctamente. Inicie sesión para continuar.");
            }

            return (false, resultado);
        }

        // ---- ROLES ----
        public List<RolDTO> GetDefaultRoles() => new()
        {
            new RolDTO { Id = 1, Nombre = "Administrador", Descripcion = "Control total del sistema", Estado = true },
            new RolDTO { Id = 2, Nombre = "Empleado", Descripcion = "Gestión de tareas internas", Estado = true },
            new RolDTO { Id = 3, Nombre = "Paciente", Descripcion = "Acceso a información médica personal", Estado = true },
            new RolDTO { Id = 4, Nombre = "Doctor", Descripcion = "Atención y gestión de pacientes", Estado = true },
            new RolDTO { Id = 5, Nombre = "Enfermera", Descripcion = "Apoyo y seguimiento médico", Estado = true }
        };

        public async Task<List<RolDTO>?> TryGetRolesFromApiAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<RolDTO>>("api/roles");
            }
            catch
            {
                return null;
            }
        }

        // ✅ Guardar token manualmente
        public async Task SetToken(string token)
        {
            _token = token;
            await _LocalStorage.SetAsync("token", token);
        }

        // ✅ Obtener token guardado
        public async Task<string?> GetToken()
        {
            if (string.IsNullOrEmpty(_token))
            {
                var localStorageResult = await _LocalStorage.GetAsync<string>("token");
                if (!localStorageResult.Success || string.IsNullOrEmpty(localStorageResult.Value))
                {
                    _token = null;
                    return null;
                }
                _token = localStorageResult.Value;
            }
            return _token;
        }

        // ✅ Verificar autenticación
        public async Task<bool> IsAuthenticated()
        {
            var token = await GetToken();
            return !string.IsNullOrEmpty(token) && !IsTokenExpired(token);
        }

        // ✅ Verificar expiración del token
        public bool IsTokenExpired(string token)
        {
            try
            {
                var jwtToken = new JwtSecurityToken(token);
                return jwtToken.ValidTo < DateTime.UtcNow;
            }
            catch
            {
                return true; // Si el token no es válido, se considera expirado
            }
        }

        // ✅ Cerrar sesión
        public async Task Logout()
        {
            _token = null;
            await _LocalStorage.DeleteAsync("token");
            _navManager.NavigateTo("/login", forceLoad: true);
        }
    }
}



