using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stupify.Model;
using Stupify.Services;
using System;
using System.IO;
using System.Reflection;

namespace Stupify
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                });
            services.AddControllersWithViews();

            string connectionString = "Host=localhost;Port=5432;Database=music;Username=postgres;Password=postgres";
            if (environment.IsEnvironment("Testing"))
            {
                connectionString = "Host=localhost;Port=5432;Database=music1;Username=postgres;Password=postgres";
            }
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));
            services.AddTransient<IRepository<Artist>, ArtistService>();
            services.AddTransient<IRepository<Song>, SongService>();
            services.AddTransient<IRepository<User>, UserService>();
            services.AddTransient<IRepository<UserLike>, UserLikeService>();

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}