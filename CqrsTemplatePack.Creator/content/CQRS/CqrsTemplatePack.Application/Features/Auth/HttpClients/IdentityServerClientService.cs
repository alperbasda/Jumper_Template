﻿//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper template tool. 
//---------------------------------------------------------------------------------------
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Models.Responses;
using CqrsTemplatePack.Application.Features.Auth.Commands.Login;
using CqrsTemplatePack.Application.Features.Auth.Commands.RefreshToken;
using CqrsTemplatePack.Common.IdentityConfigurations;
using Newtonsoft.Json;
using System.Text;

namespace CqrsTemplatePack.Application.Features.Auth.HttpClients
{
    public class IdentityServerClientService : IIdentityServerClientService
    {
        private HttpClient _httpClient;
        IdentityApiConfiguration _identityApiConfig;

        public IdentityServerClientService(IdentityApiConfiguration identityApiConfig, HttpClient httpClient)
        {
            _identityApiConfig = identityApiConfig;
            _httpClient = httpClient;
        }

        public async Task<Response<LoginResponse>> CreateToken(LoginCommand request)
        {
            var data = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var res = await _httpClient.PostAsync(_identityApiConfig.GetTokenAddress, data);

            var apiResponse = await res.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response<LoginResponse>>(apiResponse);
            
            return result;
        }

        public async Task<Response<RefreshTokenResponse>> RefreshToken(string refreshToken)
        {
            var res = await _httpClient.GetAsync($"{_identityApiConfig.RefreshTokenAddress}?refreshToken={refreshToken}");

            var apiResponse = await res.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response<RefreshTokenResponse>>(apiResponse);

            return result;
        }

        public async Task<Response<NoContent>> RevokeRefreshToken(string refreshToken)
        {
            
            var res = await _httpClient.GetAsync($"{_identityApiConfig.RevokeTokenAddress}/{refreshToken}");

            var apiResponse = await res.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response<NoContent>>(apiResponse);

            return result;
        }

    }
}
