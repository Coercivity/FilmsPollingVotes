using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LobbyMVC.FilmPollingDataService
{
    public class FilmPollingDataClient : IFilmPollingDataClient
    {
        private readonly HttpClient _httpClient;


        private readonly string _urlWinnerPrefix;
        private readonly string _urlPositionsPrefix;

        public FilmPollingDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _urlWinnerPrefix = configuration.GetSection("FilmPollingAPISettings:winnerUrl").Value;
            _urlPositionsPrefix = configuration.GetSection("FilmPollingAPISettings:positionsUrl").Value;

        }

        public Task AddFilmAsync(PollingFilmModel film)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PollingFilmModel>> GetFilmsByLobbyIdAsync(Guid id)
        {
            var url = _urlPositionsPrefix + id.ToString();

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var film = response.Content.ReadAsAsync<List<PollingFilmModel>>().Result;
                return film;
            }

            return null;
        }

        public Task<Guid> GetWinnerByLobbyIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFilmByIdAsync(Guid id, Guid lobbyId)
        {
            throw new NotImplementedException();
        }
    }
}
