using CineSplain.API.Models.TMBD;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CineSplain.API.Utilities;

public static class ApiUtility {
    private static readonly string _tmdbApiKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
    private static readonly string _omdbApiKey = Environment.GetEnvironmentVariable("OMDB_API_KEY");
    private static readonly string _tmdbBaseUrl = Environment.GetEnvironmentVariable("TMDB_API_URL");
    private static readonly string _omdbBaseUrl = Environment.GetEnvironmentVariable("OMDB_API_URL");
    

    private static HttpResponseMessage GetAPIResponse(string baseUrl, string apiKey, string endpoint) {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        return client.GetAsync(endpoint).Result;
    }

    public static T GetTMDBResponse<T>(string endpoint, Dictionary<string, string>? queryParams = null) {
        var defaultQueryParams = new Dictionary<string, string> {
            { "include_adult", "false" },
            { "language", "en" },
        };

        var defaultQueryString = BuildQueryString(defaultQueryParams);
        var queryString = BuildQueryString(queryParams);
        var fullEndpoint = $"{endpoint}?{defaultQueryString}" + (queryString != null ? $"&{queryString}" : "");
        var response = GetAPIResponse(_tmdbBaseUrl, _tmdbApiKey, fullEndpoint);

        if (response.IsSuccessStatusCode) {
            var jsonSerializerOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            };

            var responseContent = response.Content.ReadAsStringAsync().Result;
            var deserializedContent = JsonSerializer.Deserialize<T>(responseContent, jsonSerializerOptions);

            if (deserializedContent != null) {
                return deserializedContent;
            }
        }

        throw new Exception($"Error: {response.StatusCode}");
    }

    public static T GetOMDBResponse<T>(string imdbId) {
        var response = GetAPIResponse(_omdbBaseUrl, _omdbBaseUrl, $"?apikey={_omdbApiKey}&i={imdbId}");

        if (response.IsSuccessStatusCode) {

            var responseContent = response.Content.ReadAsStringAsync().Result;
            var deserializedContent = JsonSerializer.Deserialize<T>(responseContent);

            if (deserializedContent != null) {
                return deserializedContent;
            }
        }

        throw new Exception($"Error: {response.StatusCode}");
    }

    private static string? BuildQueryString(Dictionary<string, string>? queryParams) {
        if (queryParams == null || queryParams.Count == 0) {
            return null;
        }

        var keyValuePairs = queryParams
            .Where(kv => !string.IsNullOrEmpty(kv.Value))
            .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value ?? string.Empty)}");

        return string.Join("&", keyValuePairs);
    }

    public static string GetFormattedDate(DateTime date, string format = "yyyy-MM-dd") {
        string year = date.Year.ToString();
        string month = date.Month.ToString().PadLeft(2, '0');
        string day = date.Day.ToString().PadLeft(2, '0');

        return format.Replace("yyyy", year).Replace("MM", month).Replace("dd", day);
    }

    public static IEnumerable<ListDisplayMovieCrewCredit> CombineCrewCredits(List<ListDisplayMovieCrewCredit> crewCredits) {
        Dictionary<int, ListDisplayMovieCrewCredit> uniqueMovies = [];

        foreach (var credit in crewCredits.Where(credit => !uniqueMovies.TryAdd(credit.Id, credit))) {
            uniqueMovies[credit.Id].Job = $"{uniqueMovies[credit.Id].Job}, {credit.Job}";
            uniqueMovies[credit.Id].Department = $"{uniqueMovies[credit.Id].Department}, {credit.Department}";
        }

        return uniqueMovies.Values.ToList();
    }
}