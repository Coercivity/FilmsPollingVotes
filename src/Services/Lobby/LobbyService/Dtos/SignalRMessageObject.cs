using Domain.Entities;

namespace LobbyService.Dtos
{
    public class SignalRMessageObject
    {
        public Film Film { get; set; }

        public User User { get; set; }
    }
}
