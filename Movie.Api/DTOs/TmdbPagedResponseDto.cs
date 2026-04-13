namespace Movie.Api.DTOs
{
    public class TmdbPagedResponseDto<T>
    {
        public int Page { get; set; }
        public List<T> Results { get; set; } = new();
    }
}