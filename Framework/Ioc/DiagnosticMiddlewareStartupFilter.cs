using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Framework.Ioc
{
    public partial class DiagnosticMiddlewareStartupFilter : IStartupFilter
    {
        private readonly IHostingEnvironment _env;

        public DiagnosticMiddlewareStartupFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                // Use a terminal middleware that branches on a request for
                // /services. The middleware uses a factory to obtain the services
                // registered for the app and outputs them in a webpage.
                app.Map("/services", builder => builder.Run(async ctx =>
                {
                    // Make sure this only runs in the Development environment.
                    if (!_env.IsDevelopment())
                    {
                        return;
                    }

                    var sb = new StringBuilder(@"
                        <!DOCTYPE html><html lang=""en""><head><title>All Services</title>
                        <style>body{font-family:Verdana,Geneva,sans-serif;font-size:.8em}
                        li{padding-bottom:10px}</style></head><body>
                        <h1>All Services</h1>
                        <ul>");

                    var serviceDescriptorService =
                        ctx.RequestServices.GetService<IServiceDescriptorsService>();

                    foreach (var service in serviceDescriptorService.GetServices())
                    {
                        sb.Append($"<li><b>{service.FullName}</b> ({service.Lifetime})");
                        if (!string.IsNullOrEmpty(service.ImplementationType))
                        {
                            sb.Append($"<br>{service.ImplementationType}</li>");

                        }
                        else
                        {
                            sb.Append($"</li>");
                        }

                    }

                    sb.Append("</ul></body></html>");

                    await ctx.Response.WriteAsync(sb.ToString());
                }));

                app.UseMiddleware<DiagnosticMiddleware>();

                next(app);
            };
        }
    }
}