﻿using CineSplain.API.Models.TMBD;
using CineSplain.API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CineSplain.API.Controllers;

[Route("[controller]")]
[ApiController]
public class PeopleController : ControllerBase {

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Person> GetPerson(string id) {
        try {
            var queryParams = new Dictionary<string, string> {
                { "append_to_response", "images,movie_credits" },
            };

            var person = ApiUtility.GetTMDBResponse<Person>($"person/{id}", queryParams);
            return Ok(person);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }
}