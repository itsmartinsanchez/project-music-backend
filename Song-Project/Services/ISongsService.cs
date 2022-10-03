namespace Song_Project.Services;

using Song_Project.Models;

public interface ISongsService
{
    public List<Song> GetAll();
    public Song FindById(int id);
    public Song FindBySongTitle(string title);
    public Song Save(Song songs);
    public Song Save(Dictionary<string, object> hash);
    public bool Exists(int id);

}