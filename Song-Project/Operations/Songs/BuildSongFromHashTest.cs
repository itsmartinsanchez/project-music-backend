namespace Song_Project.Operations.Songs;
using Song_Project.Models;

public class BuildSongFromHashes
{
    public Dictionary<string, object> Hash { get; set; }
    public SongModel SongTest {get; set;}
    public BuildSongFromHashes(Dictionary<string, object> hash)
    {
        Hash = hash;
        SongTest = new SongModel();
    }

    public void run()
    {
        if(Hash.GetValueOrDefault("id") != null) {
            SongTest.Id = Int32.Parse(Hash["id"].ToString());
        }

        if(Hash.GetValueOrDefault("title") != null) {
            SongTest.Title = Hash["title"].ToString();
        }
        
        if(Hash.GetValueOrDefault("artistId") != null) {
            SongTest.ArtistId = Int32.Parse(Hash["artistId"].ToString());
        }

        if(Hash.GetValueOrDefault("lyrics") != null) {
           SongTest.Lyrics = Hash["lyrics"].ToString();
        }

        if(Hash.GetValueOrDefault("album") != null) {
           SongTest.Album = Hash["album"].ToString();
        }
        
        // if(Hash.GetValueOrDefault("artistName") != null){
        //     Songs.ArtistName = Hash["artistName"].ToString();
        // }
    }
}