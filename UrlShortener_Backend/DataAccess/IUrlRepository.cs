using System;
namespace UrlShortener_Backend.DataAccess
{
    public interface IUrlRepository
    {
        Task<string> SaveShortUrl(string originalUrl, string shortUrl);

        Task<bool> IsCodeAvailable(string shortCode);

        Task<string> GetOriginalUrl(string shortUrl);

    }
}

