namespace Song_Project.Operations.Comments;

using Song_Project.Models;
using Song_Project.Services;
using System.Text.Json;

public class ValidateSaveComment : Validator
{
    public Int32 Id {get; set;}
    public String Content {get; set;}
    public Int32 Rating {get; set;}
    public Int32 UserId {get; set;}
    public Int32 SongId {get; set;}

    public void InitializeParameters(Dictionary<string, object> hash)
    {
        if(hash.GetValueOrDefault("id") != null) {
            this.Id = Int32.Parse(hash["id"].ToString());
        }
        if(hash.GetValueOrDefault("content") != null) {
            this.Content = hash["content"].ToString();
        }
        if(hash.GetValueOrDefault("rating") != null) {
            this.Rating = Int32.Parse(hash["rating"].ToString());
        }
        if(hash.GetValueOrDefault("userId") != null) {
            this.UserId = Int32.Parse(hash["userId"].ToString());
        }
        if(hash.GetValueOrDefault("songId") != null) {
            this.SongId = Int32.Parse(hash["songId"].ToString());
        }

    }
    private readonly ICommentsService _commentsService;
    private readonly IUserService _usersService;
    private readonly ISongsService _songsService;
    public ValidateSaveComment(ICommentsService commentsService, IUserService usersService, ISongsService songsService)
    {
        _commentsService = commentsService;
        _usersService = usersService;
        _songsService = songsService;

    }
    public ValidateSaveComment(Dictionary<string, object> hash, ICommentsService commentsService, IUserService usersService, ISongsService songsService)
    {
        _commentsService = commentsService;
        _usersService = usersService;
        _songsService = songsService;
        this.InitializeParameters(hash);
    }
    public override void run()
        {
        if(this.Content == null || this.Content.Equals("")) {
            String msg = "Comment is required";
            this.AddError(msg, "content");
        }
        if(this.Rating == null || this.Rating.Equals("")) {
            String msg = "Rating is required";
            this.AddError(msg, "rating");
        }
        if(this.UserId == null || this.UserId.Equals("")) {
            String msg = "User Id is required";
            this.AddError(msg, "userId");
        // } else if(!_usersService.Exists(this.UserId)) {
        //     String msg = "User not found";
        //     this.AddError(msg, "userId");
        }
        if(this.SongId == null || this.SongId.Equals("")) {
            String msg = "Song Id is required";
            this.AddError(msg, "songId");
        } else if(!_songsService.Exists_Test(this.SongId)) {
            String msg = "Song not found";
            this.AddError(msg, "songId");
        }

        }
}