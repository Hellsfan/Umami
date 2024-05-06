using Umami.Models.Domain;
using Umami.Models.ViewModels;

namespace Umami.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
    }
}
