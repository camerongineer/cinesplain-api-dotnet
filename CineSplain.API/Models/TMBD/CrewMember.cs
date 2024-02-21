namespace CineSplain.API.Models.TMBD;

public class CrewMember : Person, ICrewCredit {
    public string CreditId { get; set; }
    public string Department { get; set; }
    public int Gender { get; set; }
    public string KnownForDepartment { get; set; }
    public string Job { get; set; }
    public string Name { get; set; }
    public string OriginalName { get; set; }
}