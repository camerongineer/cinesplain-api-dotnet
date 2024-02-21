using System.ComponentModel.DataAnnotations;

namespace CineSplain.API.Models.TMBD;

public class ListDisplayMovieCrewCredit: ListDisplayMovie, ICrewCredit {
    [Required] public string CreditId { get; set; }
    public string Department { get; set; }
    public string Job { get; set; }
}