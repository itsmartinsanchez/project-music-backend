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
            //this.FindById_Test(s.Id);
        }
        else
        {
            //update
            Song temp = this.FindById_Test(s.Id);
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

    public Song FindById_Test(int id)
    {
        var test = (from s in _dataContext.Song
                    join a in _dataContext.Artist
                    on s.ArtistId equals a.Id
                    where s.Id == id
                    select new Song{
                        Id = s.Id,
                        Title = s.Title,
                        ArtistId = s.ArtistId,
                        Lyrics = s.Lyrics,
                        Album = s.Album,
                        ArtistName = a.Name
                    }).First();

        return test;            
    }
    public List<Song> GetAll_Test()
    {
        var test = (from s in _dataContext.Song
                    join a in _dataContext.Artist
                    on s.ArtistId equals a.Id
                    select new Song{
                        Id = s.Id,
                        Title = s.Title,
                        ArtistId = s.ArtistId,
                        Lyrics = s.Lyrics,
                        Album = s.Album,
                        ArtistName = a.Name
                    }).ToList();

        return test;            
    }
    public bool Exists_Test(int id)
    {
        var test = (from s in _dataContext.Song
                    join a in _dataContext.Artist
                    on s.ArtistId equals a.Id
                    where s.Id == id
                    select new Song{
                        Id = s.Id,
                        Title = s.Title,
                        ArtistId = s.ArtistId,
                        Lyrics = s.Lyrics,
                        Album = s.Album,
                        ArtistName = a.Name
                    }).First();

         return test != null;
    }

    public Song Save_Test(Song s)
    {
        SqlConnection connection = new SqlConnection(
        ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "INSERT INTO Song (Album, ArtistId, Lyrics, Title)" + 
                        "VALUES (@Album, @ArtistId, @Lyrics, @Title);";

        if(s.Id != null) {
        // UPDATE
        sql = "UPDATE Song SET Album=@Album, ArtistId=@ArtistId, Lyrics=@Lyrics, Title=@Title WHERE Id=@Id";
        }
        SqlCommand command = new SqlCommand(sql, connection);
        
        if(s.Id != null) {
        command.Parameters.AddWithValue("@Id", s.Id);
        }

        command.Parameters.AddWithValue("@Album", s.Album);
        command.Parameters.AddWithValue("@ArtistId", s.ArtistId);
        command.Parameters.AddWithValue("@Lyrics", s.Lyrics);
        command.Parameters.AddWithValue("@Title", s.Title);

        command.ExecuteNonQuery();

        connection.Close();

        return s;

    }

    // public SongModel SaveTest(Dictionary<string, object> hash)
    // {
    //     var builder = new BuildSongFromHashes(hash);
    //     builder.run();

    //     return this.Save(builder.SongTest);
    // }
}