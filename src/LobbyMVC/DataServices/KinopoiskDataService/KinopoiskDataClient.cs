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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        private string _urlFilmPrefix;
        private readonly string _urlParameters;
        private readonly string _token;

        public KinopoiskDataClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            _urlFilmPrefix = _configuration.GetSection("KinopoiskAPISettings:url").Value;
            _urlParameters = _configuration.GetSection("KinopoiskAPISettings:urlParameters").Value;
            _token = _configuration.GetSection("KinopoiskAPISettings:token").Value;
        }

        public  async Task<Film> GetFilmAttributes(string link)
        {
            var url = _urlFilmPrefix + link;

            var httpClient = _httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri(url);


            httpClient.DefaultRequestHeaders.Add("X-API-KEY", _urlParameters);
            httpClient.DefaultRequestHeaders.Add("Bearer", _token);
            httpClient.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("application/json"));


            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var film = response.Content.ReadAsAsync<Film>().Result;
                return film;
            }


            return null;

        }
    }
}
