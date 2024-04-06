using Microsoft.AspNetCore.Mvc;
using UrlShortener_Backend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UrlShortener_Backend.Controllers
{
    [Route("/")]
    public class UrlShortener : Controller
    {
        private readonly UrlService _urlService;
        private readonly string baseUrl;

        public UrlShortener(UrlService urlService)
        {
            _urlService = urlService;
            baseUrl = _urlService.GetBaseUrl();
        }


        /// <summary>
        /// Get ShortUrl
        /// </summary>
        /// <param name="shortUrl">Code of short url</param>
        /// <returns>Original Url</returns>
        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> Get(string shortUrl)
        {
            try
            {
                if (!_urlService.IsValidShortUrl(shortUrl))
                {
                    return BadRequest("Not valid shortUrl");
                }

                string originalUrl = await _urlService.GetOriginalUrl(shortUrl);
                return Ok(originalUrl);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Shorten Url
        /// </summary>
        /// <param name="originalUrl">Original url</param>
        /// <returns>shortUrl</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string originalUrl)
        {
            try
            {
                string shortCode = await _urlService.ShortenUrl(originalUrl);
                return Ok(baseUrl + shortCode);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

