namespace CineSplain.API.Models.TMBD;

public class CastMember {
    public bool Adult { get; set; }
    public string BackdropPath { get; set; }
    public int CastId { get; set; }
    public string Character { get; set; }
    public string CreditId { get; set; }
    public int Gender { get; set; }
    public int[] GenreIds { get; set; }
    public int Id { get; set; }
    public string KnownForDepartment { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public string OriginalLanguage { get; set; }
    public string OriginalName { get; set; }
    public string OriginalTitle { get; set; }
    public string Overview { get; set; }
    public double Popularity { get; set; }
    public string PosterPath { get; set; }
    public string ProfilePath { get; set; }
    public string ReleaseDate { get; set; }
    public string Title { get; set; }
    public bool Video { get; set; }
    public double VoteAverage { get; set; }
    public int VoteCount { get; set; }
}