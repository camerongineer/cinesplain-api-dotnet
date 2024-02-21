namespace CineSplain.API.Models.TMBD;

public interface ICrewCredit {
    int Id { get; set; }
    public string CreditId { get; set; }
    string Job { get; set; }
    string Department { get; set; }
}