namespace LALAPATAPA.Services.Authentication
{
    public interface IAvatarUrlProvider
    {
        string GetAvatarUrl(string email);
    }
}
