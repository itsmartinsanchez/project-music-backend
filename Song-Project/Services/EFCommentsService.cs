namespace Song_Project.Services;

using Song_Project.Models;
using Song_Project.Data;
using Song_Project.Operations.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class EFCommentsService : ICommentsService
{
    private readonly DataContext _dataContext;
    public EFCommentsService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool Delete(Comment comment)
    {
       if (comment != null && comment.Id > 0)
        {
            _dataContext.Comment.Remove(comment);
            _dataContext.SaveChanges();

            return true;
        }
        else
        {
            return false;
        }
    }

    public Comment FindById(int id)
    {
       var temp = (_dataContext.Comment.SingleOrDefault(c => c.Id == id));
       
       return temp; 
    }

    public Comment FindById_Test(int id)
    {
            var temp = (from c in _dataContext.Comment
                        join u in _dataContext.User
                        on c.UserId equals u.Id
                        where c.Id == id
                        select new Comment{
                            Id = c.Id,
                            Content = c.Content,
                            Rating = c.Rating,
                            UserId = c.UserId,
                            SongId = c.SongId,
                            Username = u.Username
                        }).First();

       return temp; 

    }
       
    public List<Comment> FindBySongId(int songId)
    {
       return _dataContext.Comment.Where(s => s.SongId == songId).ToList();
    }

    public List<Comment> FindBySongId_Test(int songId)
    {
        var temp =  (   from c in _dataContext.Comment
                        join u in _dataContext.User
                        on c.UserId equals u.Id
                        where c.SongId == songId
                        select new Comment{
                            Id = c.Id,
                            Content = c.Content,
                            Rating = c.Rating,
                            UserId = c.UserId,
                            SongId = c.SongId,
                            Username = u.Username
                        }).ToList();

       return temp;
    }

    public List<Comment> GetAll()
    {
        return _dataContext.Comment.ToList();
    }

    public List<Comment> GetAll_Test()
    {
        var temp = (from c in _dataContext.Comment
                        join u in _dataContext.User
                        on c.UserId equals u.Id
                        select new Comment{
                            Id = c.Id,
                            Content = c.Content,
                            Rating = c.Rating,
                            UserId = c.UserId,
                            SongId = c.SongId,
                            Username = u.Username
                        }).ToList();

       return temp;
    }

    public Comment Save(Comment c)
    {
        if(c.Id == null || c.Id == 0)
        {
            //create
            _dataContext.Comment.Add(c);
        }
        else
        {
            //update
            Comment temp = this.FindById(c.Id);
            temp.SongId = c.SongId;
            temp.Rating = c.Rating;
            temp.UserId = c.UserId;
            temp.Content = c.Content;
        }

        _dataContext.SaveChanges();

        return c;
    }

    public Comment Save(Dictionary<string, object> hash)
    {
        var builder = new BuildCommentFromHash(hash);
        builder.run();

        return this.Save_Test(builder.Comments);
    }

    public Comment Save_Test(Comment c)
    {
        SqlConnection connection = new SqlConnection(
        ApplicationManager.Instance.GetConnectionString()
        );
        String sql = "";
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter(); 
       

        connection.Open();
        System.Console.WriteLine("SQL Query:");

        sql =           "INSERT INTO Comment (Content, Rating, UserId, SongId)" + 
                        "VALUES (@Content, @Rating, @UserId, @SongId);"+
                        "SELECT Comment.Content, Comment.Rating, Comment.Id, Comment.UserId, Comment.SongId, [User].Username from Comment " +
                        "INNER Join [User] ON Comment.UserId=[User].Id";
                        
        command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Content", c.Content);
        command.Parameters.AddWithValue("@Rating", c.Rating);
        command.Parameters.AddWithValue("@UserId",c.UserId);
        command.Parameters.AddWithValue("@SongId", c.SongId);
        command.ExecuteNonQuery();
        connection.Close();

        return c;
    }
}