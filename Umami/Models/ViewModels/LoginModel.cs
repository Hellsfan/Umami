using System.ComponentModel.DataAnnotations;

namespace Umami.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
