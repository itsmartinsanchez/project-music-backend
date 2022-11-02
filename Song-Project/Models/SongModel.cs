namespace Song_Project.Models;
using Song_Project.Services;

public class SongModel
{
    private readonly IArtistsService _artistsService;
    public Int32 Id {get; set;}
    public String Title {get; set;}
    public Int32 ArtistId {get; set;}
    public string Lyrics {get; set;}
    public string Album {get; set;}
    public SongModel(){}
}