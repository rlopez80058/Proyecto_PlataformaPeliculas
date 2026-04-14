using Movie.Api.Client;

namespace Movie.Api.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly ITmdbClient _tmdbClient;

        public TmdbService(ITmdbClient tmdbClient)
        {
            _tmdbClient = tmdbClient;
        }
    }
}