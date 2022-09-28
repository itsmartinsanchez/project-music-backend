namespace Song_Project.Operations.Artists;

using Song_Project.Models;
using Song_Project.Operations;

public class ValidateGetArtist : Validator
{
    public Artists Artists { get; private set; }
    public ValidateGetArtist(Artists artists)
    {
        Artists = artists;
    }

    public override void run()
    {
        if(this.Artists == null) {
            String msg = "Artist not found";
            this.AddError(msg, "id");
        }
    }
}