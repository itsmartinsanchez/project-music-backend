namespace Song_Project.Models;

public class Songs
{
    public Int32 Id {get; set;}
    public String Title {get; set;}
    public Int32 ArtistId {get; set;}
    public string Lyrics {get; set;}
    public string Album {get; set;}
    public Songs(){}
    public Songs(String title, string lyrics, string album)
    {
        this.Title = title;
        this.Lyrics =lyrics;
        this.Album = album;
    }
    public Songs(Int32 id, String title, string lyrics, string album)
        : this(title, lyrics, album)
    {
        this.Id = id;
    }    

}