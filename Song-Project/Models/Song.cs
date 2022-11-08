namespace Song_Project.Models;
using Song_Project.Services;

public class Song
{
    private readonly IArtistsService _artistsService;
    public Int32 Id {get; set;}
    public String Title {get; set;}
    public Int32 ArtistId {get; set;}
    public string Lyrics {get; set;}
    public string Album {get; set;}
    public string? ArtistName {get; set;}
    //public ICollection<Artist> Artists { get; set; }
    public Song(){}
    public Song(string title, string lyrics, string album)
    {
        this.Title = title;
        this.Lyrics =lyrics;
        this.Album = album;
        //this.ArtistName = artistName;
    } 

}