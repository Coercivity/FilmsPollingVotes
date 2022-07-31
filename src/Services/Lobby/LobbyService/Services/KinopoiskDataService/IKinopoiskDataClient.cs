using Domain.Entities;
using System.Threading.Tasks;

namespace LobbyService.KinopoiskDataService
{
    public interface IKinopoiskDataClient
    {
        Task<Film> GetFilmAttributes(string id);
    }
}
