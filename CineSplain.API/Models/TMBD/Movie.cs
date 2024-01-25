namespace CineSplain.API.Models.TMBD;

public class Movie {
    public bool Adult { get; set; }
    public string BackdropPath { get; set; }
    public object BelongsToCollection { get; set; }
    public int Budget { get; set; }
    public List<Genre> Genres { get; set; }
    public List<int> GenreIds { get; set; }
    public string Homepage { get; set; }
    public int Id { get; set; }
    public string ImdbId { get; set; }
    public MovieImageCategory Images { get; set; }
    public string MediaType { get; set; }
    public string OriginalLanguage { get; set; }
    public string OriginalTitle { get; set; }
    public string Overview { get; set; }
    public double Popularity { get; set; }
    public string PosterPath { get; set; }
    public List<ProductionCompany> ProductionCompanies { get; set; }
    public List<ProductionCountry> ProductionCountries { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public long Revenue { get; set; }
    public int Runtime { get; set; }
    public List<SpokenLanguage> SpokenLanguages { get; set; }
    public string Status { get; set; }
    public string Tagline { get; set; }
    public string Title { get; set; }
    public Video? Trailer { get; set; }
    public bool Video { get; set; }
    public VideoCategory Videos { get; set; }
    public double VoteAverage { get; set; }
    public int VoteCount { get; set; }
}