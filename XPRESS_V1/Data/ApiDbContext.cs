using Microsoft.EntityFrameworkCore;
using XPRESS_V1.Models;
using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }


        public DbSet<TestTable> TestTables { get; set; } 
        public DbSet<Advait> Advait { get; set; }
        public DbSet<Test_Tables_George> TestTables_George { get; set; }
        public DbSet<Riona> Riona { get; set; }
        public DbSet<Mahesh> Maheshs { get; set; }

    }
}
