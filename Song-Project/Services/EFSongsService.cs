namespace Song_Project.Services;

using Song_Project.Models;
using Song_Project.Data;
using Song_Project.Operations.Songs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class EFSongsService : ISongsService
{
    private readonly DataContext _dataContext;
    public EFSongsService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool Exists(int id)
    {
         return _dataContext.Songs.SingleOrDefault(s => s.Id == id) != null;
    }

    public Songs FindById(int id)
    {
       Songs temp = _dataContext.Songs.SingleOrDefault(s => s.Id == id);
       
       return temp;
    }

    public Songs FindBySongTitle(string title)
    {
        Songs temp = _dataContext.Songs.SingleOrDefault(s => s.Title == title);
       
       return temp;
    }

    public List<Songs> GetAll()
    {
        return _dataContext.Songs.ToList();
    }

    public Songs Save(Songs songs)
    {
        if(songs.Id == null || songs.Id == 0)
        {
            _dataContext.Songs.Add(songs);
        }

        _dataContext.SaveChanges();

        return songs;
    }

    public Songs Save(Dictionary<string, object> hash)
    {
        var builder = new BuildSongFromHash(hash);
        builder.run();

        return this.Save(builder.Songs);
    }
}