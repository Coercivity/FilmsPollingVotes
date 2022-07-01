using Domain.Entities;
using LobbyMVC.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LobbyMVC.Services.IdentityService
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _key;
        private readonly string _registerSuffix; 
        private readonly string _authentificationSuffix; 

        public IdentityService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _key = _configuration.GetSection("IdentityAPISettings:JwtKey").Value;
            _registerSuffix = _configuration.GetSection("IdentityAPISettings:register").Value;
            _authentificationSuffix = _configuration.GetSection("IdentityAPISettings:authentificate").Value;
        }

        private IEnumerable<Claim> GetTokenClaims(string token)
        {

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Empty string is given");
            }
            try
            {
                var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_key))
                };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();


                ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters,
                                                                                        out SecurityToken securityToken);

                return claimsPrincipal.Claims;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<IEnumerable<Claim>> AuthorizeUserAsync(AuthentificateUserDto authentificateUserDto)
        {

            var url = _httpClient.BaseAddress + _authentificationSuffix;

            var message = new HttpRequestMessage(HttpMethod.Post, url);


            message.Content = new StringContent(
                JsonSerializer.Serialize(authentificateUserDto), 
                Encoding.UTF8, 
                "application/json");


            var response = await _httpClient.SendAsync(message);


            if (response.IsSuccessStatusCode)
            {
                var token = response.Content.ReadAsAsync<string>().Result;
                return GetTokenClaims(token);

            }

            return null;

        }

        public async Task RegisterUserAsync(User user)
        {

            var url = _httpClient.BaseAddress + _registerSuffix;

            var httpContent = new StringContent(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, httpContent);


            response.EnsureSuccessStatusCode();
        }
    }
}










