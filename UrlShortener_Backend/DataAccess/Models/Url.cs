namespace UrlShortener_Backend.DataAccess.Models
{
    public class Url
    {
        public Url()
        {

        }

        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}

