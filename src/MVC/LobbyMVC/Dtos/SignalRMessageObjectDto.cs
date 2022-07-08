using Domain.Entities;

namespace LobbyMVC.Dtos
{
    public class SignalRMessageObjectDto
    {
        public Film Film { get; set; }

        public User User { get; set; }
    }
}
