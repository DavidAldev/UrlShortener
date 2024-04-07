using System;
namespace UrlShortener_Backend.Services
{
    public interface IUrlService
    {
        Task<string> ShortenUrl(string originalUrl);

        Task<string> GetOriginalUrl(string shortUrl);

        bool IsValidShortUrl(string shortUrl);

        bool IsValidUrl(string url);

        string GetBaseUrl();

    }
}

