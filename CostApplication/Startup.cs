using CostApplication.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using CostApplication.DTO;
using CostApplication.Repositories;
using UserApplication.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CostApplication.Auth;

namespace CostApplication
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
            services.AddDbContext<AppDBContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped<IAppDBContext, AppDBContext>();


            services.AddScoped<ICostRepository, CostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            var key = Encoding.UTF8.GetBytes("somekey");

            services.AddAuthentication().
                AddScheme<CookieTokenAuthenticationOptions, CustomHandler>("Default", o => {});

            services.AddScoped<ICustomTokenService, CustomTokenService>();

            services.AddControllersWithViews();
            services.AddMvc(options => {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((_) => "The field is required.");
            });
            services.AddAutoMapper(typeof(Startup));
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
