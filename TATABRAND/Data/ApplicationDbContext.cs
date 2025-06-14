using Microsoft.EntityFrameworkCore;
using TATABRAND.Models;

namespace TATABRAND.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        public DbSet<TataPk> tatatable { get; set; }
    }
}
