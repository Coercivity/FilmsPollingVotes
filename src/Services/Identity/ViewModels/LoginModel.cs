using System.ComponentModel.DataAnnotations;

namespace IdentityService.ViewModels
{
    public record LoginModel
    {
        [Required]
        public string Login { get; init; }
        [Required]
        public string Password { get; init; }
        public string ReturnUrl { get; init; }

    }
}
