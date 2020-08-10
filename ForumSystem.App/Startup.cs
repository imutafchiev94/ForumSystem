using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ForumSystem.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using ForumSystem.App.Services.SendGrid;
using ForumSystem.App.Seeder;
using ForumSystem.App.Common;
using ForumSystem.App.Areas.Admin.Services.Interfaces;
using ForumSystem.App.Areas.Admin.Services;
using ForumSystem.Models;
using ForumSystem.App.Services.Interface;
using ForumSystem.App.Services;

namespace ForumSystem.App
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
            services.AddDbContext<ForumSystemDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ForumSystemDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.AddScoped<IAdminTopicService, AdminTopicService>();
            services.AddScoped<IAdminPostsService, AdminPostsService>();
            services.AddScoped<IAdminCommentsServices, AdminCommentsService>();
            services.AddScoped<IHomeServices, HomeService>();
            services.AddScoped<ITopicsServices, TopicsService>();
            services.AddScoped<IPostsServices, PostsServices>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 6,
                    RequireUppercase = true,
                    RequireLowercase = true,
                    RequiredUniqueChars = 0,
                    RequireNonAlphanumeric = false
                };



                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 3;

            });
            services.AddSingleton<ISendGridEmailSender, SendGridEmailSender>();

            services.Configure<SendGridOptions>(this.Configuration.GetSection("EmailSender"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ForumSystemDbContext>();
                dbContext.Database.Migrate();
                new RoleSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

                endpoints.MapAreaControllerRoute(
                    name: "area", GlobalConstants.AdminstrationRoleName,
                    pattern: "{area:exists}/{cotroller}/{action=Index}/{id?}");

            });
        }
    }
}
