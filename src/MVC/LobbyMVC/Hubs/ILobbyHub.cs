using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LobbyMVC.Hubs
{
    public interface ILobbyHub
    {
        Task SendEntityPositionAsync(string userName, Film film);

        Task UpdateUsersAsync(IEnumerable<string> users);
    }
}