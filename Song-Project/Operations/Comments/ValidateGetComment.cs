namespace Song_Project.Operations.Comments;

using Song_Project.Models;
using Song_Project.Operations;

public class ValidateGetComment : Validator
{
    public Comments Comments {get; set;}
    public ValidateGetComment(Comments c)
    {
        Comments = c;
    }
    public override void run()
    {
        if(Comments == null)
        {
            string m = "Comments not found";
            Messages.Add(m);

            Dictionary<string, object> mHash = new Dictionary<string, object>();
            mHash["comments"] = m;

            MessageHashes.Add(mHash);
        }
    }
}