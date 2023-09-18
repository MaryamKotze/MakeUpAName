using System;
using System.Collections.Generic;
using MakeUpAName.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MakeUpAName.Models;
namespace MakeUpAName
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            if (connectionString != null)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            }
            else
            {
                // Handle the case where the connection string is missing.
                // You might want to log an error, throw an exception, or provide a default connection 
                //string.
            }
            // Other service registrations...
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
       ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configure error handling for non-development environments
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            // Configure the rest of your app
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}