namespace Song_Project.Services;

using Song_Project.Models;

public interface ISongsService
{
    public List<Song> GetAll();
    public Song FindById(int id);
    public Song FindBySongTitle(string title);
    public Song Save(Song songs);
    public Song Save(Dictionary<string, object> hash);
    public Song Update(Dictionary<string, object> hash);
    public bool Exists(int id);
    public bool Delete(Song song);
    public Song FindById_Test(int id);
    public List<Song> GetAll_Test();
    public Song Save_Test(Song song);
    public Song Update_Test(Song song);
    public bool Exists_Test(int id);
    //public SongModel SaveTest(Dictionary<string, object> hash);

}