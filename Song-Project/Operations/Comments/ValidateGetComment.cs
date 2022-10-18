namespace Song_Project.Operations.Comments;

using Song_Project.Models;
using Song_Project.Operations;

public class ValidateGetComment : Validator
{
    public Comment Comment {get; set;}
    public ValidateGetComment(Comment comments)
    {
        Comment = comments;
    }
    public override void run()
    {
        if(this.Comment == null)
        {
            String msg = "Comment not found";
            this.AddError(msg, "id");
        }
    }
}