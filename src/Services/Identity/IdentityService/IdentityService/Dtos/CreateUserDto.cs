using System.ComponentModel.DataAnnotations;

namespace IdentityService.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; } 
    }
}
