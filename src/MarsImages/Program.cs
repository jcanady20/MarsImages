using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MarsImages
{
    public class Program
    {
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public static async Task Main(string[] args)
        {
            var hostBuilder = CreateWebHostBuilder(args).Build();
            var cacheService = hostBuilder.Services.GetRequiredService<Internal.Services.IPhotoCacheService>();
            cacheService.InitializeCache();
            await cacheService.PreFetchImageMetaDataAsync();
            await cacheService.CacheImagesAsync();
            hostBuilder.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                );
    }
}
