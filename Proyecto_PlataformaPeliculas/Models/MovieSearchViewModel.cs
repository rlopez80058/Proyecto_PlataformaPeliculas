namespace Proyecto_PlataformaPeliculas.Models
{
    public class MovieSearchViewModel
    {
        public string? Query { get; set; }
        public int? GenreId { get; set; }
        public string? Language { get; set; }
        public int? Year { get; set; }
        public List<int> Years { get; set; } = new();
        public List<string> Languages { get; set; } = new();

        public List<MovieViewModel> Results { get; set; } = new();
        public List<GenreViewModel> Genres { get; set; } = new();
    }
}