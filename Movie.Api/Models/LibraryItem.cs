namespace Movie.Api.Models
{
    public enum MovieStatus
    {
        Pending = 1,
        Watching = 2,
        Watched = 3,
        Dropped = 4
    }
    public class LibraryItem
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movies Movie { get; set; } = null!;

        public MovieStatus Status { get; set; } = MovieStatus.Pending;
        public int? Rating { get; set; }  
        public string? Comment { get; set; }

        public DateTime AddedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
