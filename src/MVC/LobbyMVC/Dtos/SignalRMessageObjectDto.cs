using Domain.Entities;

namespace LobbyMVC.Dtos
{
    public class SignalRMessageObject
    {
        public Film Film { get; set; }

        public User User { get; set; }
    }
}
