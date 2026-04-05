namespace Movie.Api.Models
{
    public class MovieGenre
    {
        public int MovieId { get; set; }
        public Movies Movie { get; set; } = null!;

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}
