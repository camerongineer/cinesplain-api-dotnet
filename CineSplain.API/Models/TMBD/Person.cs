namespace CineSplain.API.Models.TMBD;

public class Person {
    public bool Adult { get; set; }
    public List<string> AlsoKnownAs { get; set; }
    public string Biography { get; set; }
    public DateTime? Birthday { get; set; }
    public DateTime? Deathday { get; set; }
    public int Gender { get; set; }
    public string Homepage { get; set; }
    public int Id { get; set; }
    public string ImdbId { get; set; }
    public string KnownForDepartment { get; set; }
    public string Name { get; set; }
    public string PlaceOfBirth { get; set; }
    public double Popularity { get; set; }
    public string ProfilePath { get; set; }
    public PersonImageCategory Images { get; set; }
    public Credits MovieCredits { get; set; }
}