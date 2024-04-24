using Lab10.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace Lab10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MusicDbContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            
            var app = builder.Build();

            // Додавання даних у базу даних
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MusicDbContext>();

                dbContext.Database.Migrate();
                if (!dbContext.MusicTracks.Any())
                {
                    dbContext.MusicTracks.AddRange(
                        new MusicTrack { Title = "Song 1", Artist = "Artist 1", ReleaseDate = DateTime.Parse("2024-04-15") },
                        new MusicTrack { Title = "Song 2", Artist = "Artist 2", ReleaseDate = DateTime.Parse("2024-04-16") },
                        new MusicTrack { Title = "Song 3", Artist = "Artist 3", ReleaseDate = DateTime.Parse("2024-04-17") }
                    );

                    dbContext.SaveChanges();
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=MusicTrack}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
