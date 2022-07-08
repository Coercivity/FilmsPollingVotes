using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LobbyMVC.KinopoiskDataService
{

    public class KinopoiskDataClient : IKinopoiskDataClient
    {
        private readonly HttpClient _httpClient;


        public KinopoiskDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
        }

        public async Task<Film> GetFilmAttributes(string link)
        {
            var url = _httpClient.BaseAddress + link;

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return response.Content.ReadAsAsync<Film>().Result;


        }
    }
}
