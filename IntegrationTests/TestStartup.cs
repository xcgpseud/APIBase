using APIBase;
using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void SetUpDatabase(IServiceCollection services)
        {
            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<MainContext>(options =>
                    options.UseSqlite(@"DataSource=testdb.db;"));
        }

        public override void Migrate(MainContext dbContext)
        {
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();
        }
    }
}