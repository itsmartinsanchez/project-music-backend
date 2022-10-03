namespace Song_Project.Operations.Comments;

using Song_Project.Models;

public class BuildCommentFromHash
{
    public Dictionary<string, object> Hash { get; set; }
    public Comment Comments  {get; set; }
    public BuildCommentFromHash(Dictionary<string, object> hash)
    {
        Hash = hash;
        Comments = new Comment();
    }

    public void run()
    {
         if(Hash.GetValueOrDefault("id") != null) {
            Comments.Id = Int32.Parse(Hash["id"].ToString());
        }

        if(Hash.GetValueOrDefault("content") != null) {
            Comments.Content = Hash["content"].ToString();
        }

        if(Hash.GetValueOrDefault("rating") != null) {
            Comments.Rating = Int32.Parse(Hash["rating"].ToString());
        }
        
        if(Hash.GetValueOrDefault("userId") != null) {
            Comments.UserId = Int32.Parse(Hash["userId"].ToString());
        }

        if(Hash.GetValueOrDefault("songId") != null) {
            Comments.SongId = Int32.Parse(Hash["songId"].ToString());
        }
    }
}