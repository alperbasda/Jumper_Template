using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CqrsTemplatePack.Persistence.Contexts;

public class JumperDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }

    public JumperDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {

        Configuration = configuration;
        Database.EnsureCreated();
    }


    //IEntityTypeConfiguration dan kalıtılan tüm konfigurasyonları işler.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
