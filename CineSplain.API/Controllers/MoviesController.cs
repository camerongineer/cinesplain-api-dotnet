using CineSplain.API.Models.OMDB;
using CineSplain.API.Models.TMBD;
using CineSplain.API.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CineSplain.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : Controller {

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<FullDisplayMovie> GetMovie(int id) {
        try {
            var queryParams = new Dictionary<string, string> {
                { "append_to_response", "images,videos" },
            };


            var movie = ApiUtility.GetTMDBResponse<FullDisplayMovie>($"movie/{id}", queryParams);

            if (movie?.Videos?.Results != null) {

                var trailers = movie.Videos.Results
                    .Where(video => video.Type == "Trailer" || video.Type == "Teaser")
                    .ToList();

                trailers.Sort((a, b) => {
                    var aIsOfficial =
                        a.Official && a.Name.Contains("trailer", StringComparison.CurrentCultureIgnoreCase);

                    var bIsOfficial =
                        b.Official && b.Name.Contains("trailer", StringComparison.CurrentCultureIgnoreCase);

                    if (a.Type == "Teaser" && b.Type != "Teaser") {
                        return 1;
                    } else if (b.Type == "Teaser" && a.Type != "Teaser") {
                        return -1;
                    } else {
                        return (aIsOfficial == bIsOfficial) ? 0 : (aIsOfficial ? -1 : 1);
                    }
                });

                movie.Trailer = trailers.FirstOrDefault();
            }

            return Ok(movie);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }

    [HttpGet("{id:int}/Credits")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MovieCreditCategory> GetMovieCredits(int id) {
        try {
            var credits = ApiUtility.GetTMDBResponse<MovieCreditCategory>($"movie/{id}/credits");
            return Ok(credits);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }

    [HttpGet("{id:int}/Similar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MovieListPage> GetSimilarMovies(int id) {
        try {
            var similarMovies = ApiUtility.GetTMDBResponse<MovieListPage>($"movie/{id}/similar");
            return Ok(similarMovies);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }

    [HttpGet("{id:int}/Recommended")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MovieListPage> GetRecommendedMovies(int id) {
        try {
            var recommendedMovies = ApiUtility.GetTMDBResponse<MovieListPage>($"movie/{id}/recommendations");
            return Ok(recommendedMovies);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }

    [HttpGet("Search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MovieListPage> SearchMovies([FromQuery] string query, [FromQuery] int page = 1) {
        try {
            var queryParams = new Dictionary<string, string> {
                { "query", query },
                { "page", $"{page}" }
            };

            var movieList = ApiUtility.GetTMDBResponse<MovieListPage>($"search/movie", queryParams);
            return Ok(movieList);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }

    [HttpGet("NowPlaying")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [OutputCache(Duration = 21600)]
    public ActionResult<MovieListPage> GetNowPlayingMovies() {
        try {
            var movieList = ApiUtility.GetTMDBResponse<MovieListPage>($"movie/now_playing");
            return Ok(movieList);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }

    [HttpGet("Discover")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [OutputCache(Duration = 21600)]
    public ActionResult<MovieListPage> GetDiscoverMovies() {
        try {
            var movieList = ApiUtility.GetTMDBResponse<MovieListPage>($"discover/movie");
            return Ok(movieList);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }

    [HttpGet("Upcoming")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [OutputCache(Duration = 21600)]
    public ActionResult<MovieListPage> GetUpcomingMovies() {
        try {
            var queryParams = new Dictionary<string, string> {
                { "primary_release_date.gte", ApiUtility.GetFormattedDate(DateTime.Now) },
                { "primary_release_date.lte", ApiUtility.GetFormattedDate(DateTime.Now + TimeSpan.FromHours(2190)) },
                { "sort_by", "popularity.desc" },
                { "with_original_language", "en" }
            };

            var movieList = ApiUtility.GetTMDBResponse<MovieListPage>($"discover/movie", queryParams);
            return Ok(movieList);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }

    [HttpGet("Classics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [OutputCache(Duration = 21600)]
    public ActionResult<MovieListPage> GetClassicMovies() {
        try {
            var queryParams = new Dictionary<string, string> {
                { "sort_by", "vote_average.desc" },
                { "vote_count.gte", "1000" },
                { "without_genres", "99,10755" },
                { "with_original_language", "en" }
            };

            var movieList = ApiUtility.GetTMDBResponse<MovieListPage>($"discover/movie", queryParams);
            return Ok(movieList);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }

    [HttpGet("MostLoved")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [OutputCache(Duration = 21600)]
    public ActionResult<MovieListPage> GetMostLovedMovies() {
        try {
            var queryParams = new Dictionary<string, string> {
                { "primary_release_date.gte", ApiUtility.GetFormattedDate(DateTime.Now - TimeSpan.FromHours(2190)) },
                { "sort_by", "vote_average.desc" },
                { "vote_count.gte", "20" },
                { "without_genres", "99,10755" },
                { "with_original_language", "en" }
            };

            var movieList = ApiUtility.GetTMDBResponse<MovieListPage>($"discover/movie", queryParams);
            return Ok(movieList);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }

    [HttpGet("MostHated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [OutputCache(Duration = 21600)]
    public ActionResult<MovieListPage> GetMostHatedMovies() {
        try {
            var queryParams = new Dictionary<string, string> {
                { "primary_release_date.gte", ApiUtility.GetFormattedDate(DateTime.Now - TimeSpan.FromHours(2190)) },
                { "sort_by", "vote_average.asc" },
                { "vote_count.gte", "20" },
                { "without_genres", "99,10755" },
                { "with_original_language", "en" }
            };

            var movieList = ApiUtility.GetTMDBResponse<MovieListPage>($"discover/movie", queryParams);
            return Ok(movieList);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }

    [HttpGet("OMDB{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OmdbMovieDetails> GetOmdbMovieDetails(string id) {
        try {
            var omdbMovieDetails = ApiUtility.GetOMDBResponse<OmdbMovieDetails>(id);
            var ratingDetails = new OmdbMovieRatingDetails();

            ratingDetails.ImdbRating =
                double.TryParse(omdbMovieDetails.imdbRating, out var iResult) ? iResult : (double?)null;

            ratingDetails.Metascore = int.TryParse(omdbMovieDetails.Metascore, out var mResult) ? mResult : (int?)null;

            var rottenTomatoesScore = omdbMovieDetails.Ratings
                .FirstOrDefault(rating => rating.Source == "Rotten Tomatoes")?.Value;

            ratingDetails.RottenTomatoesScore =
                int.TryParse(rottenTomatoesScore?.Trim('%'), out var rResult) ? rResult : (int?)null;

            omdbMovieDetails.RatingDetails = ratingDetails;

            return Ok(omdbMovieDetails);
        } catch (Exception e) {
            Console.WriteLine(e);
        }

        return NotFound();
    }
}