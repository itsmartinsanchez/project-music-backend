namespace Song_Project.Operations.Artists;

using Song_Project.Models;
using Song_Project.Operations;

public class ValidateGetArtist : Validator
{
    public Artist Artist { get; private set; }
    public ValidateGetArtist(Artist artists)
    {
        Artist = artists;
    }

    public override void run()
    {
        if(this.Artist == null) {
            String msg = "Artist not found";
            this.AddError(msg, "id");
        }
    }
}