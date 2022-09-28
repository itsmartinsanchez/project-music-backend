namespace Song_Project.Services;

using Song_Project.Models;
using Song_Project.Data;
using Song_Project.Operations.Artists;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

public class EFArtistsService : IArtistsService
{
    private readonly DataContext _dataContext;

    public EFArtistsService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Artists Delete(Artists a)
    {
        if(a.Id == null || a.Id == 0)
        {
            _dataContext.Artists.Remove(a);
        } else {
            Artists temp = this.FindById(a.Id);
            temp.Name = a.Name;
        }

        _dataContext.SaveChanges();

        return a;
    }

    public bool Exists(int id)
    {
        return _dataContext.Artists.SingleOrDefault(a => a.Id == id) != null;
    }

    public bool Exists(string name)
    {
         return _dataContext.Artists.SingleOrDefault(a => a.Name.Equals(name)) != null;
    }

    public Artists FindById(int id)
    {
        Artists temp = _dataContext.Artists.SingleOrDefault(a => a.Id == id);
        
        return temp;
    }

    public Artists FindByName(string name)
    {
        if(name != null) {
            var pName = new SqlParameter(
                "name",
                name
            );

            Artists a = _dataContext.Artists
                            .FromSqlRaw("SELECT * FROM dbo.Artists WHERE Name=@name", pName)
                            .SingleOrDefault();

            return a;
        }

        return null;
    }

    public List<Artists> GetAll()
    {
        return _dataContext.Artists.ToList();

        
    }
    public Artists Save(Artists a)
    {
        if(a.Id == null || a.Id == 0)
        {
            _dataContext.Artists.Add(a);
        } else {
            Artists temp = this.FindById(a.Id);
            temp.Name = a.Name;
        }

        _dataContext.SaveChanges();

        return a;
    }

    public Artists Save(Dictionary<string, object> hash)
    {
        var builder = new BuildArtistFromHash(hash);
        builder.run();

        return this.Save(builder.Artists);
    }
}