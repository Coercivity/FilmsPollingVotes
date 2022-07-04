using Application.Contracts;
using Domain.Entities;
using LobbyMVC.Dtos;
using LobbyMVC.FilmPollingDataService;
using LobbyMVC.KinopoiskDataService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LobbyMVC.Controllers
{
    public class LobbyController : Controller
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IKinopoiskDataClient _kinopoiskDataClient;
        private readonly IFilmPollingDataClient _filmPollingDataClient;

        public LobbyController(IMeetingRepository meetingRepository, IKinopoiskDataClient kinopoiskDataClient,
                               IFilmPollingDataClient filmPollingDataClient)
        {
            _meetingRepository = meetingRepository;
            _kinopoiskDataClient = kinopoiskDataClient;
            _filmPollingDataClient = filmPollingDataClient;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CreateLobby(string name)
        {
            var meeting = new Meeting()
            {
                Id = Guid.NewGuid(),
                Name = name
 
            };

            return View("Lobby");
        }




        public async Task<ActionResult> GetFilmListAsync()
        {
            var lobbyId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var filmId = Guid.Parse("3fa85f64-5717-4566-b3fc-2c963f66afa6");


            var films = await _filmPollingDataClient.GetFilmsByLobbyIdAsync(lobbyId);

            var t = await _filmPollingDataClient.GetWinnerByLobbyIdAsync(lobbyId);
            await _filmPollingDataClient.RemoveFilmByIdAsync(filmId, lobbyId);
            if(films is null)
            {
                return NotFound();
            }

            return View("index", films);

        }

        public async Task<ActionResult> AddFilmAsync(string link)
        {
            var film = await _kinopoiskDataClient.GetFilmAttributes(link);

            var lobbyId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa1");

            var creatorId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa2");

            var filmId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa3");




            var filmToSend = new PollingModel() { 
                CreatorId = creatorId,
                EntityId = filmId,
                MeetingId = lobbyId,
                CreatorWeight = 1
            };

            await _filmPollingDataClient.AddFilmAsync(filmToSend);

            return View("index", film);
        }


    }
}
