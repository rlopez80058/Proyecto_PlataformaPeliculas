namespace Movie.Api.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public int TmdbGenreId { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
