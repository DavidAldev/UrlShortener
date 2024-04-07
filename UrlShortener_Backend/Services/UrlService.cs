using System.Data.SqlTypes;
using UrlShortener_Backend.DataAccess;

namespace UrlShortener_Backend.Services
{
    public class UrlService : IUrlService
    {
        private readonly UrlRepository _urlRepository;
        private readonly int lenghtCode = 6;
        private readonly Lazy<char[]> charsForShortening;
        private readonly Lazy<Random> random;

        public UrlService(UrlShortenerDbContext context)
        {
            _urlRepository = new(context);
            random = new Lazy<Random>(() => new Random());
            charsForShortening = new Lazy<char[]>(GenerateCharsForShortening);
        }

        public async Task<string> ShortenUrl(string originalUrl)
        {
            bool codeAvailable;
            string shortUrl;
            do
            {
                shortUrl = GenerateRandomCode();
                codeAvailable = await _urlRepository.IsCodeAvailable(shortUrl);
            } while (!codeAvailable);

            return await _urlRepository.SaveShortUrl(originalUrl, shortUrl);
        }

        public async Task<string> GetOriginalUrl(string shortUrl)
        {
            return await _urlRepository.GetOriginalUrl(shortUrl);
        }

        public bool IsValidShortUrl(string shortUrl)
        {
            return shortUrl is not null && shortUrl.Length == lenghtCode;
        }

        public bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult);
        }

        public string GetBaseUrl()
        {
            var request = new HttpContextAccessor()?.HttpContext?.Request;
            if (request is not null)
            {
                return $"{request.Scheme}://{request.Host}{request.PathBase}/";
            }

            throw new Exception("Error creating base url");
        }

        private string GenerateRandomCode()
        {
            char[] randomString = new char[lenghtCode];
            for (int i = 0; i < lenghtCode; i++)
            {
                randomString[i] = charsForShortening.Value[random.Value.Next(charsForShortening.Value.Length)];
            }
            return new string(randomString);
        }

        private char[] GenerateCharsForShortening()
        {
            char[] alphabet = Enumerable.Range('a', 26)
                                        .Select(x => (char) x)
                                        .ToArray();
            char[] numbersFrom0To9 = Enumerable.Range('0', 10)
                                               .Select(x => (char) x)
                                               .ToArray();

            return alphabet.Concat(numbersFrom0To9).ToArray();
        }
    }
}

