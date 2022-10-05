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
         return _dataContext.Song.SingleOrDefault(s => s.Id == id) != null;
    }

    public Song FindById(int id)
    {
       Song temp = _dataContext.Song.SingleOrDefault(s => s.Id == id);
       
       return temp;
    }

    public Song FindBySongTitle(string title)
    {
        Song temp = _dataContext.Song.SingleOrDefault(s => s.Title == title);
       
       return temp;
    }

    public List<Song> GetAll()
    {
        return _dataContext.Song.ToList();
    }

    public Song Save(Song songs)
    {
        if(songs.Id == null || songs.Id == 0)
        {
            _dataContext.Song.Add(songs);
        }

        _dataContext.SaveChanges();

        return songs;
    }

    public Song Save(Dictionary<string, object> hash)
    {
        var builder = new BuildSongFromHash(hash);
        builder.run();

        return this.Save(builder.Songs);
    }
}