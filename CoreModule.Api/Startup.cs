using CoreModule.Repositories;
using CoreModule.Repository;
using CoreModule.Repository.Repositories;
using Framework.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: ModuleStartup(typeof(CoreModule.Api.Startup))]
namespace CoreModule.Api
{
    public class Startup
    {
        public void Configuration(IApplicationBuilder app, IServiceCollection services)
        {
            services.AddDbContext<CoreModuleContext>(
                options => options.UseSqlite("Data Source=BlogDatabase.db", 
                optionsBuilder => optionsBuilder.MigrationsAssembly("CoreModule.Repository")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<ICoreRepository, CoreRepository>();
            
            app.UseAuthentication();

        }
    }
}
