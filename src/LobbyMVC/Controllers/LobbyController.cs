using Application.Contracts;
using Domain.Entities;
using LobbyMVC.Dtos;
using LobbyMVC.FilmPollingDataService;
using LobbyMVC.KinopoiskDataService;
using Microsoft.AspNetCore.Http;
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


        public async Task<ActionResult> Create(string link)
        {
            var film = await _kinopoiskDataClient.GetFilmAttributes(link);

            var lobbyId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

           var films = _filmPollingDataClient.GetFilmsByLobbyIdAsync(lobbyId);

            return View("index", film);
        }


    }
}
