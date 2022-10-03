namespace Song_Project.Operations.Artists;

using Song_Project.Models;

public class BuildArtistFromHash
{
    public Dictionary<string, object> Hash { get; set; }
    public Artist Artists  {get; set; }
    public BuildArtistFromHash(Dictionary<string, object> hash)
    {
        Hash = hash;
        Artists = new Artist();
    }

    public void run()
    {
        if(Hash.GetValueOrDefault("id") != null) {
            Artists.Id = Int32.Parse(Hash["id"].ToString());
        }

        if(Hash.GetValueOrDefault("name") != null) {
            Artists.Name = Hash["name"].ToString();
        }
    }
}