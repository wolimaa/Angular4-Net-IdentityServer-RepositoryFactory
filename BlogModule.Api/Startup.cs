using BlogModule.Repositories;
using BlogModule.Repository;
using BlogModule.Repository.Repositories;
using Framework.Attributes;
using Framework.Ioc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: HostingStartup(typeof(BlogModule.Api.Startup))]

namespace BlogModule.Api
{
    public class Startup
    {
        public void Configuration(IWebHostBuilder builder)
        {

            builder.ConfigureServices(services =>
            {
                Func<IServiceProvider, IServiceDescriptorsService> factory =
                    provider => new ServiceDescriptorsService(services);

                services.AddSingleton(factory);
                services.AddSingleton<IStartupFilter, DiagnosticMiddlewareStartupFilter>();
                services.AddDbContext<BlogContext>(
                options => options.UseSqlite("Data Source=BlogDatabase.db",
                optionsBuilder => optionsBuilder.MigrationsAssembly("BlogModule.Repository")));

                services.AddTransient<IBlogRepository, BlogRepository>();

            });



        }
    }
}
