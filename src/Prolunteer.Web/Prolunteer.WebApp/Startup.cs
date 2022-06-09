using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prolunteer.BusinessLogic.Base;
using Prolunteer.Common.Configurations;
using Prolunteer.DataAccess;
using Prolunteer.DataAccess.EntityFramework;
using Prolunteer.Notifications.Email;
using Prolunteer.Notifications.NotificationManager;
using Prolunteer.WebApp.Code;
using Prolunteer.WebApp.Code.ExtensionMethods;

namespace Prolunteer.WebApp
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews(options => 
            {
                options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
            });
            services.AddAutoMapper(options =>
            {
                options.AddMaps(typeof(Startup), typeof(BaseService));
            });
            services.AddDbContext<ProlunteerContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProlunteerDatabase"));
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped<UnitOfWork>();

            services.Configure<SMTPConfig>(Configuration.GetSection("SmtpConfig"));
            services.AddScoped<NotificationManager>();

            services.AddProlunteerBusinessLogic();
            services.AddProlunteerCurrentUser();
            services.AddProlunteerCustomAutoMapperLogic();

            services.AddScoped<MailService>();
            services.AddScoped<NotificationManager>();

            services.AddPresentation();

            services.AddAuthentication("ProlunteerCookies")
                .AddCookie("ProlunteerCookies", options =>
                {
                    options.AccessDeniedPath = new PathString("/Home/Index");
                    options.LoginPath = new PathString("/UserAccount/Login");
                });
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
