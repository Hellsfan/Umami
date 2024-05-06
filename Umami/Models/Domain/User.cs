using Microsoft.AspNetCore.Identity;

namespace Umami.Models.Domain
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
