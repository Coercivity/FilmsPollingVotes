using System.ComponentModel.DataAnnotations;

namespace LobbyService.Dtos
{
    public class AuthentificateUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
