using LALAPATAPA.Models;
using System.Threading.Tasks;

namespace LALAPATAPA.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }

        User AuthenticatedUser { get; }

        Task<bool> LoginAsync(string email, string password);

      
        Task<bool> UserIsAuthenticatedAndValidAsync();

        Task LogoutAsync();
    }
}
