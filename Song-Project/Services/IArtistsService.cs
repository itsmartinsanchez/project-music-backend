namespace Song_Project.Services;

using Song_Project.Models;

public interface IArtistsService
{
    public List<Artists> GetAll();
    public Artists FindById(int id);
    public bool Exists(int id);
    public bool Exists(String name);
    public Artists FindByName(string name);
    public Artists Save(Artists a);
    public Artists Save(Dictionary<string, object> hash);
    public Artists Delete(Artists a);
}