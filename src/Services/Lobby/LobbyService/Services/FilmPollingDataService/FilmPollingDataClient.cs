using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LobbyService.FilmPollingDataService
{
    public class FilmPollingDataClient : IFilmPollingDataClient
    {
        private readonly HttpClient _httpClient;


        private readonly string _winnerUrlPrefix;
        private readonly string _positionsUrlPrefix;

        public FilmPollingDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _winnerUrlPrefix = configuration.GetSection("FilmPollingAPISettings:winnerUrl").Value;
            _positionsUrlPrefix = configuration.GetSection("FilmPollingAPISettings:positionsUrl").Value;

        }

        public async Task AddFilmAsync(PollingModel film)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(film),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(_httpClient.BaseAddress.ToString(), httpContent);


            response.EnsureSuccessStatusCode();

        }

        public async Task<IEnumerable<Film>> GetFilmsByLobbyIdAsync(Guid id)
        {
            var url = _httpClient.BaseAddress + _positionsUrlPrefix + id.ToString();

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response.Content.ReadAsAsync<IEnumerable<Film>>().Result;

        }

        public async Task<Guid> GetWinnerByLobbyIdAsync(Guid id)
        {
            var url = _httpClient.BaseAddress + _winnerUrlPrefix + id.ToString();

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return Guid.Empty;
            }

            return response.Content.ReadAsAsync<Guid>().Result;
        }

        public async Task RemoveFilmByIdAsync(Guid id, Guid lobbyId)
        {
            var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress.ToString() 
                                                  + $"{lobbyId.ToString()}/{id.ToString()}");

            response.EnsureSuccessStatusCode();
        }
    }
}
