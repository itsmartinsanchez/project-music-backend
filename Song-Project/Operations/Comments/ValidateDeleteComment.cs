namespace Song_Project.Operations.Comments;

using Song_Project.Models;
using Song_Project.Services;
using System.Text.Json;

public class ValidateDeleteComment : Validator
{
    public Comment Comment {get; set;}
    private readonly ICommentsService _commentsService;
    public ValidateDeleteComment(ICommentsService commentsService)
    {
        _commentsService = commentsService;
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