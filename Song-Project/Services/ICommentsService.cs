namespace Song_Project.Services;

using Song_Project.Models;

public interface ICommentsService
{
    public List<Comments> GetAll();
    public Comments FindById(int id);
    public Comments Save(Comments comments);
    public Comments Save(Dictionary<string, object> hash);
}