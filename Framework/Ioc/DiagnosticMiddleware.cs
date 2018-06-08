using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Framework.Ioc
{
    public partial class DiagnosticMiddlewareStartupFilter
    {
        // Use a middleware to write out diagnostic information from the app.
        public class DiagnosticMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILoggerFactory _loggerFactory;
            private readonly IHostingEnvironment _env;
            private string cr = Environment.NewLine;

            public DiagnosticMiddleware(RequestDelegate next,
                                        ILoggerFactory loggerFactory,
                                        IHostingEnvironment env)
            {
                _next = next;
                _loggerFactory = loggerFactory;
                _env = env;
            }

            public async Task Invoke(HttpContext ctx)
            {
                var path = ctx.Request.Path;

                // Make sure this only runs at the /diag endpoint in the Development environment.
                if (path != "/diag" || !_env.IsDevelopment())
                {
                    await _next(ctx);
                }
                else
                {
                    var logger = _loggerFactory.CreateLogger("Requests");

                    logger.LogDebug("Received request: {METHOD} {PATH}",
                        ctx.Request.Method, ctx.Request.Path);

                    ctx.Response.ContentType = "text/plain";

                    var sb = new StringBuilder();
                    sb.Append($"{DateTimeOffset.Now}{cr}{cr}");
                    sb.Append($"Address:{cr}{cr}");
                    sb.Append($"Scheme: {ctx.Request.Scheme}{cr}");
                    sb.Append($"Host: {ctx.Request.Headers["Host"]}{cr}");
                    sb.Append($"PathBase: {ctx.Request.PathBase.Value}{cr}");
                    sb.Append($"Path: {ctx.Request.Path.Value}{cr}");
                    sb.Append($"Query: {ctx.Request.QueryString.Value}{cr}{cr}");
                    sb.Append($"Connection:{cr}{cr}");
                    sb.Append($"RemoteIp: {ctx.Connection.RemoteIpAddress}{cr}");
                    sb.Append($"RemotePort: {ctx.Connection.RemotePort}{cr}");
                    sb.Append($"LocalIp: {ctx.Connection.LocalIpAddress}{cr}");
                    sb.Append($"LocalPort: {ctx.Connection.LocalPort}{cr}");
                    sb.Append($"ClientCert: {ctx.Connection.ClientCertificate}{cr}{cr}");
                    sb.Append($"Headers:{cr}{cr}");

                    foreach (var header in ctx.Request.Headers)
                    {
                        sb.Append($"{header.Key}: {header.Value}{cr}");
                    }

                    sb.Append($"{cr}Environment Variables:{cr}{cr}");

                    var vars = Environment.GetEnvironmentVariables();
                    foreach (var key in vars.Keys.Cast<string>()
                        .OrderBy(key => key, StringComparer.OrdinalIgnoreCase))
                    {
                        sb.Append($"{key}: {vars[key]}{cr}");
                    }

                    await ctx.Response.WriteAsync(sb.ToString());
                }
            }
        }
    }
}