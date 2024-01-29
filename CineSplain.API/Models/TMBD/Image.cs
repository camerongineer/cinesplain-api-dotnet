namespace CineSplain.API.Models.TMBD;

public class Image {
    public double AspectRatio { get; set; }
    public int Height { get; set; }
    public string? Iso_639_1 { get; set; }
    public string FilePath { get; set; }
    public double VoteAverage { get; set; }
    public int VoteCount { get; set; }
    public int Width { get; set; }
}