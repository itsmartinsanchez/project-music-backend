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
       Comment temp = _dataContext.Comment.SingleOrDefault(c => c.Id == id);
       
       return temp; 
    }

    public List<Comment> FindBySongId(int songId)
    {
       return _dataContext.Comment.Where(s => s.SongId == songId).ToList();
    }

    public List<Comment> GetAll()
    {
        return _dataContext.Comment.ToList();
    }

    public Comment Save(Comment comments)
    {
        if(comments.Id == null || comments.Id == 0)
        {
            _dataContext.Comment.Add(comments);
        }

        _dataContext.SaveChanges();

        return comments;
    }

    public Comment Save(Dictionary<string, object> hash)
    {
        var builder = new BuildCommentFromHash(hash);
        builder.run();

        return this.Save(builder.Comments);
    }
}