namespace Movie.Api.Options
{
    public class TmdbOptions
    {
        public string BaseUrl { get; set; } = "https://api.themoviedb.org/3/";
        public string ReadAccessToken { get; set; } = "";
        public string ImageBaseUrl { get; set; } = "https://image.tmdb.org/t/p/w500";
    }
}
