using System;

namespace LobbyMVC.Dtos
{
    public class CreateFilmDto
    {
        public Guid CreatorId { get; set; }
        public Guid LobbyId { get; set; }
        public Guid FilmId { get; set; }
    }
}
