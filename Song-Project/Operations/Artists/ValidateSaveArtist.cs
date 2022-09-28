namespace Song_Project.Operations.Artists;

using Song_Project.Models;
using Song_Project.Services;
using System.Text.Json;

public class ValidateSaveArtists : Validator
{
    public Int32 Id { get; set; }
    public String Name { get; set; }

    public void InitializeParameters(Dictionary<string, object> hash)
    {
        if(hash.GetValueOrDefault("id") != null) {
            this.Id = Int32.Parse(hash["id"].ToString());
        }
        if(hash.GetValueOrDefault("name") != null) {
            this.Name = hash["name"].ToString();
        }
    }

    private readonly IArtistsService _artistsService;

    public ValidateSaveArtists(IArtistsService artistsService)
    {
        _artistsService = artistsService;
    }
    public ValidateSaveArtists(Dictionary<string, object> hash, IArtistsService artistsService)
    {
        _artistsService = artistsService;
        if(hash.GetValueOrDefault("id") != null) {
            this.Id = JsonSerializer.Deserialize<int>((JsonElement)hash["id"]);
        }
        if(hash.GetValueOrDefault("name") != null) {
            this.Name = hash["name"].ToString();
        }
    }

    public override void run()
    {
        if(this.Name == null || this.Name.Equals("")) {
            String msg = "Name is required";
            this.AddError(msg, "name");
        }
    }
}