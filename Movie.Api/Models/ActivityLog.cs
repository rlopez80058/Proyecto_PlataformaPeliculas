namespace Movie.Api.Models
{
    public enum ActivityType
    {
        AddedToLibrary = 1,
        StatusChanged = 2,
        Rated = 3,
        Commented = 4
    }
    public class ActivityLog
    {
        public int Id { get; set; }

        public int? MovieId { get; set; }
        public Movies? Movie { get; set; }

        public int? LibraryItemId { get; set; }
        public LibraryItem? LibraryItem { get; set; }

        public ActivityType ActionType { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
