namespace CineSplain.API.Models.TMBD;

public class MovieListPage {
    public int Page { get; set; }
    public Movie[]? Results { get; set; }
    public int TotalPages { get; set; }
    public int TotalResults { get; set; }
}