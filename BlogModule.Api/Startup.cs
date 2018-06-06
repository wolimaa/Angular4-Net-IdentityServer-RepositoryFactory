using BlogModule.Repositories;
using BlogModule.Repository;
using BlogModule.Repository.Repositories;
using Framework.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
[assembly: ModuleStartup(typeof(BlogModule.Api.Startup))]

namespace BlogModule.Api
{
    public class Startup
    {
        public void Configuration(IServiceCollection services)
        {
            services.AddDbContext<BlogContext>(
                options => options.UseSqlite("Data Source=BlogDatabase.db",
                optionsBuilder => optionsBuilder.MigrationsAssembly("BlogModule.Repository")));

            services.AddTransient<IBlogRepository, BlogRepository>();


        }
    }
}
