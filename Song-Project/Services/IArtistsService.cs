namespace Song_Project.Services;

using Song_Project.Models;

public interface IArtistsService
{
    public List<Artist> GetAll();
    public Artist FindById(int id);
    public bool Exists(int id);
    public bool Exists(String name);
    public Artist FindByName(string name);
    public Artist Save(Artist a);
    public Artist Save(Dictionary<string, object> hash);
    public Artist Delete(int id);
    public bool Delete(Artist artist);
}