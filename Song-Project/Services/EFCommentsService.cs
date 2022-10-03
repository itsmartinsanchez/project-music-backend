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
    public Comment FindById(int id)
    {
       Comment temp = _dataContext.Comments.SingleOrDefault(s => s.Id == id);
       
       return temp; 
    }

    public List<Comment> GetAll()
    {
        return _dataContext.Comments.ToList();
    }

    public Comment Save(Comment comments)
    {
        if(comments.Id == null || comments.Id == 0)
        {
            _dataContext.Comments.Add(comments);
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