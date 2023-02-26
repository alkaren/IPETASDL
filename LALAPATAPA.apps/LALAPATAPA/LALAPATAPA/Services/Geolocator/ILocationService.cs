using LALAPATAPA.Models;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LALAPATAPA.Services.Geolocator
{
    public interface ILocationService
    {
        Task<Location> GetPositionAsync();
    }
}