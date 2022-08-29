﻿using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Common;
using HiddenVilla_Client.Helper;
using Microsoft.AspNetCore.Components.Authorization;

namespace HiddenVilla_Client.Service
{
    public class AuthStateProvider: AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(SD.Local_Token);
            if (token == null)
            {
                return new AuthenticationState(
                    new ClaimsPrincipal(new ClaimsIdentity())
                );
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"))) 
        }
    }
}
