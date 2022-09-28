namespace Song_Project.Services;

using Song_Project.Models;

public interface ISongsService
{
    public List<Songs> GetAll();
    public Songs FindById(int id);
    public Songs FindBySongTitle(string title);
    public Songs Save(Songs songs);
    public Songs Save(Dictionary<string, object> hash);
    public bool Exists(int id);

}