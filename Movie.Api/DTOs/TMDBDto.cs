namespace Movie.Api.DTOs
{
    public class TmdbSearchResponseDto
    {
        public int Page { get; set; }
        public List<TmdbMovieDto> Results { get; set; } = new();
    }

    public class TmdbMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Original_Title { get; set; } = "";
        public string Overview { get; set; } = "";
        public string Original_Language { get; set; } = "";
        public string? Poster_Path { get; set; }
        public string? Backdrop_Path { get; set; }
        public string? Release_Date { get; set; }
        public decimal Popularity { get; set; }
        public decimal Vote_Average { get; set; }
        public List<int> Genre_Ids { get; set; } = new();
    }
}
