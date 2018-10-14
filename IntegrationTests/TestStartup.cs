using System.IO;
using APIBase;
using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class TestStartup : Startup
    {
        private const string DbFile = @"testdb.db";
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void SetUpDatabase(IServiceCollection services)
        {
            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<MainContext>(options =>
                    options.UseSqlite($@"DataSource={DbFile};"));
        }

        public override void Migrate(MainContext dbContext)
        {
            File.Delete(DbFile);
            base.Migrate(dbContext);
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();
        }
    }
}