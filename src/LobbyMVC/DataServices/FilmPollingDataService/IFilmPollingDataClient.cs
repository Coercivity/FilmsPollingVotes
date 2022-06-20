using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LobbyMVC.FilmPollingDataService
{
    public interface IFilmPollingDataClient
    {
        Task<Guid> GetWinnerByLobbyIdAsync(Guid id);
        Task RemoveFilmByIdAsync(Guid id, Guid lobbyId);
        Task AddFilmAsync(PollingFilmModel film);
        Task<List<PollingFilmModel>> GetFilmsByLobbyIdAsync(Guid id);

        
    }
}
