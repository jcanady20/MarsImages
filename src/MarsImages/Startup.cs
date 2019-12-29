using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace MarsImages
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
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
            services.AddSingleton<Internal.Data.IMemoryCache, Internal.Data.MemoryCache>();
            services.AddHttpClient<Internal.Services.IMarsImageHttpClient, Internal.Services.MarsImageHttpClient>();
            services.AddHttpClient<Internal.Services.IMarsImageMetaDataHttpClient, Internal.Services.MarsImageMetaDataHttpClient>(client => {
                client.BaseAddress = new System.Uri(Configuration["BaseUrl"]);
            });
            services.AddSingleton<Internal.Services.IImageMetaDataService, Internal.Services.MarsImageMetaDataService>();
            services.AddTransient<Internal.Services.IPhotoCacheService, Internal.Services.PhotoCacheService>();

            services.AddControllersWithViews()
                .AddNewtonsoftJson();
            
            services.AddHealthChecks()
                .AddCheck<Internal.HealthChecks.HealthCheck>("HealthCheck", HealthStatus.Unhealthy, new [] { "General" });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //  HTTPS is disabled as containers require a valid trusted cert.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health", new HealthCheckOptions(){
                    AllowCachingResponses = false
                });
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
