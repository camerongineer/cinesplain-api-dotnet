namespace CineSplain.API.Models.TMBD;

public class FullDisplayPerson : Person {
    public List<string> AlsoKnownAs { get; set; }
    public string Biography { get; set; }
    public string? Birthday { get; set; }
    public string? Deathday { get; set; }
    public int Gender { get; set; }
    public string? Homepage { get; set; }
    public string ImdbId { get; set; }
    public string KnownForDepartment { get; set; }
    public string Name { get; set; }
    public string? PlaceOfBirth { get; set; }
    public PersonImageCategory Images { get; set; }
    public PersonCreditCategory MovieCredits { get; set; }
}