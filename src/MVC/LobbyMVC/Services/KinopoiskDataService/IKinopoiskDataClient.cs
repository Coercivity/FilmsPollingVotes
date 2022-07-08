using Domain.Entities;
using System.Threading.Tasks;

namespace LobbyMVC.KinopoiskDataService
{
    public interface IKinopoiskDataClient
    {
        Task<Film> GetFilmAttributes(string id);
    }
}
