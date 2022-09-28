namespace Song_Project.Operations.Songs;

using Song_Project.Models;
using Song_Project.Operations;

public class ValidateGetSong : Validator
{
    public Songs Songs {get; set;}
    public ValidateGetSong(Songs s)
    {
        Songs = s;
    }
    public override void run()
    {
        if(Songs == null)
        {
            string m = "Song not found";
            Messages.Add(m);

            Dictionary<string, object> mHash = new Dictionary<string, object>();
            mHash["songs"] = m;

            MessageHashes.Add(mHash);
        }
    }
}