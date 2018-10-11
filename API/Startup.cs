using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIBase.Filters;
using Database.Context;
using Database.Repositories;
using Database.Repositories.Interfaces;
using Domain.Validators;
using FluentValidation.AspNetCore;
using FluentValidation.WebApi;
using Logic.Logics;
using Logic.Logics.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace APIBase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(typeof(ValidatorActionFilter)))
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<UserValidator>());

            SetUpDatabase(services);
            
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<MainContext>();
                Migrate(dbContext);
            }

            app.UseMvc();
        }

        #region OverrideMethods

        public virtual void SetUpDatabase(IServiceCollection services)
        {
            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<MainContext>(options =>
                    options.UseSqlite(@"DataSource=apidb.db;"));
        }

        public virtual void Migrate(MainContext dbContext)
        {
            dbContext.Database.Migrate();
        }

        #endregion
    }
}