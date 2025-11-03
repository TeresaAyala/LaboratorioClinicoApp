using LaboratorioClinicoApp.DTO;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;

namespace LaboratorioClinicoApp.Services
{
    public class AuthServices
    {
        private readonly ProtectedLocalStorage _LocalStorage;
        private readonly HttpClient _httpClient;
        private string _token;

        public AuthServices(ProtectedLocalStorage localStorage, HttpClient httpClient)
        {
            _LocalStorage = localStorage;
            _httpClient = httpClient;
        }

        //Enviar datos a endpoint login
        public async Task<string> Login(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", usuario);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<string>();

                return result;
            }

            return null;

        }

        //Guardar token en el navegador

        public async Task SetToken(string token)
        {
            _token = token;
            await _LocalStorage.SetAsync("token", token);
        }

        //Obtener token del navegador
        public async Task<string> GetToken()
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

        //Verificar si el usuario esta autenticado
        public async Task<bool> IsAuthenticated()
        {
            var token = await GetToken();

            return !string.IsNullOrEmpty(token) && !IsTokenExpired(token);
        }

        //Verificar si el token ha expirado
        public bool IsTokenExpired(string token)
        {
            var jwtToken = new JwtSecurityToken(token);
            return jwtToken.ValidTo < DateTime.UtcNow;
        }

        //Cerrar sesion
        public async Task Logout()
        {
            _token = null;
            await _LocalStorage.DeleteAsync("token");
        }

    }
}

