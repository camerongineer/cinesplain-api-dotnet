namespace CineSplain.API.Models.TMBD;

public class Video {
    public string Id { get; set; }
    public string Iso_639_1 { get; set; }
    public string Iso_3166_1 { get; set; }
    public string Key { get; set; }
    public string Name { get; set; }
    public string PublishedAt { get; set; }
    public string Site { get; set; }
    public int Size { get; set; }
    public bool Official { get; set; }
    public string Type { get; set; }
}