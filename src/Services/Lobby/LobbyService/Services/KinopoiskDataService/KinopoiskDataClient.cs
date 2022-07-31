using Domain.Entities;
using LobbyService.Helpers;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace LobbyService.KinopoiskDataService
{


    public class KinopoiskDataClient : IKinopoiskDataClient
    {
        
        private readonly HttpClient _httpClient;

        public KinopoiskDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
        }

        public async Task<Film> GetFilmAttributes(string id)
        {
            var url = _httpClient.BaseAddress.ToString();

            url += id;

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response.Content.ReadAsAsync<Film>().Result;
        }



    }
}
