namespace Song_Project.Operations.Artists;

using Song_Project.Models;
using Song_Project.Services;
using System.Text.Json;

public class ValidateDeleteArtist : Validator
{
    public Artists Artists {get; set;}

    private readonly IArtistsService _artistsService;

    public ValidateDeleteArtist(IArtistsService artistsService)
    {
        _artistsService = artistsService;
    }
    public override void run()
    {
        if(this.Artists == null) {
            String msg = "Artist not found";
            this.AddError(msg, "id");
        }
    }
}