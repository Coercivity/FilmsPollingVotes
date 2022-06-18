using System;

namespace LobbyMVC.Dtos
{
    public class CreateFilmDto
    {
        public Guid CreatorId { get; set; }
        public string Link { get; set; }
    }
}
