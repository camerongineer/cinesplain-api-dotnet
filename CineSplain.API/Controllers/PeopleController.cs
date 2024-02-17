using CineSplain.API.Models.TMBD;
using CineSplain.API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CineSplain.API.Controllers;

[Route("[controller]")]
[ApiController]
public class PeopleController : ControllerBase {

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<FullDisplayPerson> GetPerson(string id) {
        try {
            var queryParams = new Dictionary<string, string> {
                { "append_to_response", "images,movie_credits" },
            };

            var person = ApiUtility.GetTMDBResponse<FullDisplayPerson>($"person/{id}", queryParams);
            var movieCrewCredits = ApiUtility.CombineCrewCredits(person.MovieCredits.Crew);
            person.MovieCredits.Crew = (List<ListDisplayMovieCrewCredit>)movieCrewCredits;
            return Ok(person);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }
}