namespace Song_Project.Services;

using Song_Project.Models;

public interface ICommentsService
{
    public List<Comment> GetAll();
    public Comment FindById(int id);
    public List<Comment> FindBySongId(int songId);
    public Comment Save(Comment comments);
    public Comment Save(Dictionary<string, object> hash);
}