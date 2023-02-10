using FluentValidation.AspNetCore;
using GameNews.Context;
using GameNews.Models;
using GameNews.RepositoryDesingPattern.Base;
using GameNews.RepositoryDesingPattern.Concrete;
using GameNews.RepositoryDesingPattern.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace GameNews
{
    public class Startup
    {
        readonly IConfiguration _config;
        public Startup(IConfiguration config) => _config = config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(_config.GetConnectionString("Mssql")));
            services.AddScoped<IRepositoryBase<News>, RepositoryBase<News>>();
            services.AddScoped<IRepositoryBase<Contact>, RepositoryBase<Contact>>();
            services.AddScoped<IRepositoryBase<Comment>, RepositoryBase<Comment>>();
            services.AddScoped<ICommentsDto, CommenRepository>();
            services.AddTransient<CookieHelper>();

            services.AddScoped<IRepositoryBase<Category>, RepositoryBase<Category>>();
            services.AddScoped<INewsDto, NewsRepository>();

            services.AddIdentity<AppUser, AppUserRole>(x =>
            {
                x.User.RequireUniqueEmail = true;
                x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
                x.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<MyDbContext>().AddErrorDescriber<CustomIdentityErrorMessages>().AddRoles<AppUserRole>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "_user";
                options.LoginPath = "/auth/index";
                options.AccessDeniedPath = "/home";
            });
            services.AddAuthorization(options=>
            {

                options.AddPolicy("Writer", policy => policy.RequireRole("member","admin"));

            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
                app.UseHsts();
            }
            app.UseStatusCodePagesWithRedirects("/home/error?code={0}");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {


                endpoints.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "fornews",
                    pattern: "{controller=News}/{action=Index}/{title}-{id}"
                    );

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
