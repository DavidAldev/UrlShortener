using System;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener_Backend.DataAccess
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlShortenerDbContext _context;

        public UrlRepository(UrlShortenerDbContext context)
        {
            _context = context;
        }

        public async Task<string> SaveShortUrl(string originalUrl, string shortUrl)
        {
            if (originalUrl is not null &&
                shortUrl is not null)
            {
                Models.Url url = new()
                {
                    OriginalUrl = originalUrl,
                    ShortUrl = shortUrl
                };

                await _context.Urls.AddAsync(url);
                await _context.SaveChangesAsync();

                return shortUrl;
            }

            throw new Exception("Invalid data to shortener Url");

        }

        public async Task<bool> IsCodeAvailable(string shortCode)
        {
            var urlExist = await _context.Urls
                                        .Where(u => u.ShortUrl == shortCode)
                                        .FirstOrDefaultAsync();

            return urlExist is null;
        }

        public async Task<string> GetOriginalUrl(string shortUrl)
        {
            var url = await _context.Urls.Where(u => u.ShortUrl == shortUrl)
                                         .FirstOrDefaultAsync();

            if (url is not null)
            {
                return url.OriginalUrl;
            }

            throw new Exception("Not existing url with code " + shortUrl);
        }
    }
}

