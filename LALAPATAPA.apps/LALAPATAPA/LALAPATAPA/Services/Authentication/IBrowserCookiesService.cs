using System.Threading.Tasks;

namespace LALAPATAPA.Services.Authentication
{
    public interface IBrowserCookiesService
    {
        Task ClearCookiesAsync();
    }
}
