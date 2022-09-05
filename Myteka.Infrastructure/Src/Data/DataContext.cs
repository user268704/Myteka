using Microsoft.EntityFrameworkCore;
using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Audio> Audios { get; set; }
    public DbSet<Content> Content { get; set; }

    private IConfiguration Configuration { get; set; }

    public DataContext(/*IConfiguration configuration*/)
    {
        //Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>().Navigation(c => c.Metadata).AutoInclude();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql(Configuration.GetConnectionString("Npgsql"));
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Myteka.Test;Username=postgres;Password=str33tf1ght3r");
        
    }
}