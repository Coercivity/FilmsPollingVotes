using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace LobbyMVC.KinopoiskDataService
{


    public class KinopoiskDataClient : IKinopoiskDataClient
    {
        private const string LINKBASE = "https://www.kinopoisk.ru/film/";
        private readonly HttpClient _httpClient;

        public KinopoiskDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
        }

        public async Task<Film> GetFilmAttributes(string link)
        {
            var url = _httpClient.BaseAddress.ToString();

            if (IsLink(link))
            { 
                url += GetFilmIdFromLink(link);
            }
            else
            {
                url += link;
            }
            
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response.Content.ReadAsAsync<Film>().Result;
        }


        private bool IsLink(string link)
        {
            if(link.Length < LINKBASE.Length)
            {
                return false;
            }

            if(LINKBASE.Equals(link.Substring(0, LINKBASE.Length)))
            {
                return true;
            }
            return false;
        }


        private string GetFilmIdFromLink(string link)
        {
            return link.Substring(link.LastIndexOf('/', link.Length - 2) + 1);
        }
    }
}
