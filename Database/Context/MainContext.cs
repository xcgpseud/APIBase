using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Context
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }
        
        public DbSet<UserEntity> Users { get; set; }
    }
}