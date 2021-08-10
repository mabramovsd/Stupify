using Microsoft.EntityFrameworkCore;

namespace Stupify.Model
{
    /// <summary>
    /// Снимок базы
    /// </summary>
    public class ApplicationContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }


        public ApplicationContext()
        {
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}