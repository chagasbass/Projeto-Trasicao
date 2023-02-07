using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProjetoTransicao.Extensions.Logs.Entities;
using ProjetoTransicao.Extensions.Logs.Services;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace ProjetoTransicao.Extensions.Logs.Configurations
{
    public static class LogExtensions
    {
        public static Logger ConfigureStructuralLogWithSerilog(IConfiguration configuration)
        {
            return new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Error)
            .MinimumLevel.Override("System", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("healthcheck")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("healthcheck-ui")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("HealthChecksDb")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("HealthChecksUI")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("healthchecks-data-ui")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("swagger")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger/index.html")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("swagger/index.html")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger/v1/swagger.json")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("swagger/v1/swagger.json")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Key.ToString().Contains("healthz-json")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("healthz-json")))
            .Destructure.ByTransforming<HttpRequest>(x => new
            {
                x.Method,
                Url = x.Path,
                x.QueryString
            })
            .WriteTo.Console()
            .CreateLogger();
        }

        public static IServiceCollection AddFilterToSystemLogs(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {

                builder.AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning)
                       .AddFilter("Microsoft.AspNetCore", LogLevel.Warning)
                       .AddFilter("Microsoft.AspNetCore.Hosting.Diagnostics", LogLevel.Warning)
                       .AddConsole();
            });

            return services;
        }

        public static IServiceCollection AddStructuraLog(this IServiceCollection services)
        {
            services.AddSingleton<ILogServices, LogServices>();
            services.AddSingleton<LogData>();

            return services;
        }
    }
}