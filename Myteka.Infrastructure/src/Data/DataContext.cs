using Microsoft.EntityFrameworkCore;
using Myteka.Models.InternalModels;
using Myteka.Models.InternalModels.Users;

namespace Myteka.Infrastructure.Data;

public class DataContext : DbContext
{
    //public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Audio> Audios { get; set; }
    public DbSet<Content> Content { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>().Navigation(c => c.Metadata).AutoInclude();
    }
}