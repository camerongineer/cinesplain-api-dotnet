using System.ComponentModel.DataAnnotations;

namespace CineSplain.API.Models.TMBD;

public class ListDisplayMovieCastCredit: ListDisplayMovie {
    public string Character { get; set; }
    [Required] public string CreditId { get; set; }
    public int Order { get; set; }
}