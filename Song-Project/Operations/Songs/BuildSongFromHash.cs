namespace Song_Project.Operations.Songs;

using Song_Project.Models;

public class BuildSongFromHash
{
    public Dictionary<string, object> Hash { get; set; }
    public Song Songs {get; set;}
    public BuildSongFromHash(Dictionary<string, object> hash)
    {
        Hash = hash;
        Songs = new Song();
    }

    public void run()
    {
        if(Hash.GetValueOrDefault("id") != null) {
            Songs.Id = Int32.Parse(Hash["id"].ToString());
        }

        if(Hash.GetValueOrDefault("title") != null) {
            Songs.Title = Hash["title"].ToString();
        }
        
        if(Hash.GetValueOrDefault("artistId") != null) {
            Songs.ArtistId = Int32.Parse(Hash["artistId"].ToString());
        }

        if(Hash.GetValueOrDefault("lyrics") != null) {
            Songs.Lyrics = Hash["lyrics"].ToString();
        }

        if(Hash.GetValueOrDefault("album") != null) {
            Songs.Album = Hash["album"].ToString();
        }
    }
}