using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Blog
{
    public class IoCConfig
    {
        public IoCConfig()
        {
            Assembly.Load(new AssemblyName("BlogModule.Api"));
            Assembly.Load(new AssemblyName("BlogModule"));
            Assembly.Load(new AssemblyName("BlogModule.Infraestructure"));

        }



        public static void Register(IServiceCollection services)
        {



        }
        public static void Register(IApplicationBuilder app)
        {

        }
    }
}
