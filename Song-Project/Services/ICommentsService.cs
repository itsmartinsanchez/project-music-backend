namespace Song_Project.Services;

using Song_Project.Models;

public interface ICommentsService
{
    public List<Comment> GetAll();
    public Comment FindById(int id);
    public List<Comment> FindBySongId(int songId);
    public Comment Save(Comment comments);
    public List<Comment> GetAll_Test();   
    public Comment Save_Test(Comment comments);
    public Comment FindById_Test(int id);
    public List<Comment> FindBySongId_Test(int songId);
    public Comment Save(Dictionary<string, object> hash);
    public bool Delete(Comment comment);
}