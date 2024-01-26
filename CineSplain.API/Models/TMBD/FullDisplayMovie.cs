namespace CineSplain.API.Models.TMBD;

public class FullDisplayMovie : Movie {
    public Collection? BelongsToCollection { get; set; }
    public int Budget { get; set; }
    public List<Genre> Genres { get; set; }
    public string Homepage { get; set; }
    public string ImdbId { get; set; }
    public MovieImageCategory Images { get; set; }
    public List<ProductionCompany> ProductionCompanies { get; set; }
    public List<ProductionCountry> ProductionCountries { get; set; }
    public long Revenue { get; set; }
    public int Runtime { get; set; }
    public List<SpokenLanguage> SpokenLanguages { get; set; }
    public string Status { get; set; }
    public string Tagline { get; set; }
    public Video? Trailer { get; set; }
    public VideoCategory Videos { get; set; }
}