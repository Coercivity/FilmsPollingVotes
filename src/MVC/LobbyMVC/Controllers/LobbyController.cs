using Application.Contracts;
using Domain.Entities;
using LobbyMVC.FilmPollingDataService;
using LobbyMVC.Helpers;
using LobbyMVC.KinopoiskDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LobbyMVC.Controllers
{
    [Authorize]
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

        [Route("meeting/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {

            var meeting = await _meetingRepository.GetMeetingAsync(id);

            return View(meeting);
        }





        public async Task<IActionResult> CreateLobby(string lobbyName)
        {   

            var id = Guid.NewGuid();

            var link = UriHelper.BuildLink(id, Url.Action(nameof(Index), "meeting"));

            var meeting = new Meeting()
            {
                Id = id,
                Name = lobbyName,
                URL = link

            };

            await _meetingRepository.CreateMeetingAsync(meeting);

            return RedirectToAction(nameof(Index), new { id = meeting.Id });
        }


        public async Task<IActionResult> GetFilmListAsync()
        {
            var lobbyId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var filmId = Guid.Parse("3fa85f64-5717-4566-b3fc-2c963f66afa6");


            var films = await _filmPollingDataClient.GetFilmsByLobbyIdAsync(lobbyId);

            var winner = await _filmPollingDataClient.GetWinnerByLobbyIdAsync(lobbyId);

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
