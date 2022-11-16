using Boonker.Controllers;
using Boonker.Data;
using Boonker.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boonker
{
    public class Startup
    {
        private IConfigurationRoot _configStr;

        [System.Obsolete]
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostenv)
        {
            Configuration = configuration;
            _configStr = new ConfigurationBuilder().SetBasePath(hostenv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddControllersWithViews();

            //Connect postgresql
            services.AddDbContext<BooksAddData>(o => o.UseNpgsql(_configStr.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<BooksAddData>(options => options.UseSqlServer(_configStr.GetConnectionString("DefaultConnection")));


            services.AddControllersWithViews()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();

            //services.AddIdentity<User, IdentityRole<long>>().AddUserStore<BooksAddData>()
            //        .AddDefaultTokenProviders();

            services.AddIdentity<User, IdentityRole>(options => {
                options.User.RequireUniqueEmail = false;
                })
            .AddEntityFrameworkStores<BooksAddData>()
            .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();    // подключение аутентификации
            app.UseAuthorization();

            app.UseMvc(routs => {

                routs.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");

                routs.MapRoute(name: "cats", template: "Books/{action}/{category?}",
                    defaults: new { Controller = "Books", action = "List" });

                routs.MapRoute(name: "inserts", template: "Books/{action=Index}/");
                routs.MapRoute(name: "author", template: "Author/{action=CreateAuthor}/");   
                });


            //using base object with scope such as thread elements
            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    BooksAddData content = scope.ServiceProvider.GetRequiredService<BooksAddData>();
            //    DataInsert.Initial(content);
            //}
        }
    }
}
