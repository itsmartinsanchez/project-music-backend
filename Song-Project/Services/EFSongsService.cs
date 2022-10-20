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

    public bool Delete(Song song)
    {
       if (song != null && song.Id > 0)
        {
            _dataContext.Song.Remove(song);
            _dataContext.SaveChanges();

            return true;
        }
        else
        {
            return false;
        }
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

    public Song Save(Song s)
    {
        if(s.Id == null || s.Id == 0)
        {
            //create
            _dataContext.Song.Add(s);
        }
        else
        {
            //update
            Song temp = this.FindById(s.Id);
            temp.ArtistId = s.ArtistId;
            temp.Title = s.Title;
            temp.Lyrics = s.Lyrics;
            temp.Album = s.Album;
        }

        _dataContext.SaveChanges();

        return s;
    }

    public Song Save(Dictionary<string, object> hash)
    {
        var builder = new BuildSongFromHash(hash);
        builder.run();

        return this.Save(builder.Songs);
    }
}