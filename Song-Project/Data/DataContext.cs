namespace Song_Project.Data;

using Microsoft.EntityFrameworkCore;
using Song_Project.Models;

public class DataContext : DbContext
{
    public DbSet<Artist> Artist {get; set;}
    public DbSet<Song> Songs {get; set;}
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Artist>()
        .HasIndex(a => a.Name)
        .IsUnique();
        
    }
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {

    }
}