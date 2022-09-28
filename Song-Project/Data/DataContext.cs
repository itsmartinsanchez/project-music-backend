namespace Song_Project.Data;

using Microsoft.EntityFrameworkCore;
using Song_Project.Models;

public class DataContext : DbContext
{
    public DbSet<Artists> Artists {get; set;}
    public DbSet<Songs> Songs {get; set;}
    public DbSet<Users> Users { get; set; }
    public DbSet<Comments> Comments {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Artists>()
        .HasIndex(a => a.Name)
        .IsUnique();
        
    }
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {

    }
}