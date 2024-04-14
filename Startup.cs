using Lab10.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MusicDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MusicDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Додавання даних у базу даних
            if (!dbContext.MusicTracks.Any())
            {
                dbContext.MusicTracks.AddRange(
                    new MusicTrack { Title = "Song 1", Artist = "Artist 1", ReleaseDate = DateTime.Parse("2024-04-15") },
                    new MusicTrack { Title = "Song 2", Artist = "Artist 2", ReleaseDate = DateTime.Parse("2024-04-16") },
                    new MusicTrack { Title = "Song 3", Artist = "Artist 3", ReleaseDate = DateTime.Parse("2024-04-17") }
                );

                dbContext.SaveChanges();
            }
            dbContext.Database.Migrate();
        }
    }
}
