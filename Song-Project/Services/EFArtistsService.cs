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

    public Artist Delete(Artist a)
    {
        if(a.Id == null || a.Id == 0)
        {
            _dataContext.Artist.Remove(a);
        } else {
            Artist temp = this.FindById(a.Id);
            temp.Name = a.Name;
        }

        _dataContext.SaveChanges();

        return a;
    }

    public bool Exists(int id)
    {
        return _dataContext.Artist.SingleOrDefault(a => a.Id == id) != null;
    }

    public bool Exists(string name)
    {
         return _dataContext.Artist.SingleOrDefault(a => a.Name.Equals(name)) != null;
    }

    public Artist FindById(int id)
    {
        Artist temp = _dataContext.Artist.SingleOrDefault(a => a.Id == id);
        
        return temp;
    }

    public Artist FindByName(string name)
    {
        if(name != null) {
            var pName = new SqlParameter(
                "name",
                name
            );

            Artist a = _dataContext.Artist
                            .FromSqlRaw("SELECT * FROM dbo.Artists WHERE Name=@name", pName)
                            .SingleOrDefault();

            return a;
        }

        return null;
    }

    public List<Artist> GetAll()
    {
        return _dataContext.Artist.ToList();

        
    }
    public Artist Save(Artist a)
    {
        if(a.Id == null || a.Id == 0)
        {
            _dataContext.Artist.Add(a);
        } else {
            Artist temp = this.FindById(a.Id);
            temp.Name = a.Name;
        }

        _dataContext.SaveChanges();

        return a;
    }

    public Artist Save(Dictionary<string, object> hash)
    {
        var builder = new BuildArtistFromHash(hash);
        builder.run();

        return this.Save(builder.Artists);
    }
}