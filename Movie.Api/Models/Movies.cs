namespace Movie.Api.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }

        public string Title { get; set; } = null!;
        public string OriginalTitle { get; set; } = null!;
        public string Overview { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; }

        public string OriginalLanguage { get; set; } = null!;
        public string? PosterPath { get; set; }
        public string? BackdropPath { get; set; }

        public decimal Popularity { get; set; }
        public decimal VoteAverage { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
        public ICollection<LibraryItem> LibraryItems { get; set; } = new List<LibraryItem>();
    }
}
