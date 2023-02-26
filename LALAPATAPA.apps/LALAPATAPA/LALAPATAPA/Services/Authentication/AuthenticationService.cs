
using System;
using System.Threading.Tasks;

namespace LALAPATAPA.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly IBrowserCookiesService browserCookiesService;
      

        public AuthenticationService(
            IBrowserCookiesService browserCookiesService)
        {
            this.browserCookiesService = browserCookiesService;

        }

        public bool IsAuthenticated => AppSettings.User != null;

        public Models.User AuthenticatedUser => AppSettings.User;

        public Task<bool> LoginAsync(string email, string password)
        {
            var user = new Models.User
            {
                Email = email,
                Name = email,
                LastName = string.Empty,
                AvatarUrl = string.Empty,
                Token = email
            };

            AppSettings.User = user;

            return Task.FromResult(true);
        }

      

        public async Task<bool> UserIsAuthenticatedAndValidAsync()
        {
            return await Task.FromResult(IsAuthenticated);
        }

        public async Task LogoutAsync()
        {
            AppSettings.RemoveUserData();
            await browserCookiesService.ClearCookiesAsync();
        }

        
    }
}
