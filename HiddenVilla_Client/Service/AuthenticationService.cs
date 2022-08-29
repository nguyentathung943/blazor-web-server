using Blazored.LocalStorage;
using Common;
using HiddenVilla_Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace HiddenVilla_Client.Service
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly HttpClient _client;
        private ILocalStorageService localStorage;

        public AuthenticationService()
        {
            
        }

        public async Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegisteration)
        {
            // Serialize the DTO
            var content = JsonConvert.SerializeObject(userForRegisteration);

            // make request
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signup", bodyContent);

            // read content return from request
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegistrationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RegistrationResponseDTO() { IsRegisterationSuccessful = true };
            }
            else
            {
                return result;
            }
        }

        public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication)
        {
            var content = JsonConvert.SerializeObject(userFromAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signin", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await localStorage.SetItemAsync(SD.Local_Token, result.Token);
                await localStorage.SetItemAsync(SD.Local_UserDetails, result.userDTO);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new AuthenticationResponseDTO(){IsAuthSuccessful = true};
            }
            else
            {
                return result;
            }
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync(SD.Local_Token);
            await localStorage.RemoveItemAsync(SD.Local_UserDetails);
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
