using System.Threading.Tasks;

namespace LALAPATAPA.Services.Media
{
    public interface IMediaPickerService
    {
        Task<string> PickImageAsBase64String();
    }
}
