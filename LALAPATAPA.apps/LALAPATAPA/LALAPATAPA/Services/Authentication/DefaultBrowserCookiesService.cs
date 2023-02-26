using System.Threading.Tasks;

namespace LALAPATAPA.Services.Authentication
{
    public class DefaultBrowserCookiesService : IBrowserCookiesService
    {
        public Task ClearCookiesAsync() => Task.FromResult(true);
    }
}
