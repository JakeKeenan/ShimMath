using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShimMathCore.Repository;
using ShimMathCore.BL;
using ShimMathCore.Repository.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Identity;

namespace ShimMathAdmin
{
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
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                //true if the cookie has the HttpOnly attribute and cannot be accessed through a client-side script; otherwise, false. 
                //The default is false.
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; 
            });

            services.AddControllersWithViews();


            services.AddScoped<UserService>();
            services.AddScoped<UserRepo>();


            services.AddDbContextPool<ShimMathContext>(options => options.UseMySql(Configuration.GetConnectionString("ShimMathDB")));
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ShimMathContext>();
            //services.AddDefaultIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ShimMathContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
