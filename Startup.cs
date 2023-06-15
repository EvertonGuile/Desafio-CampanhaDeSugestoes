using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// Declaro meu using para corrigir problema "MyContext"
using aula_5.Context;
// Declaro para corrigir problema "UseSqlServer"
using Microsoft.EntityFrameworkCore;
// Corrigir problema "services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);"
using Microsoft.AspNetCore.Mvc;
using aula_5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace aula_5
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
            services.AddControllersWithViews();
            services.AddDbContext<MyContext>(options=>
            options.UseSqlServer(Configuration.GetConnectionString("MyConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddControllers().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_5_0);

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            // Aula 7
            services.AddIdentity<Usuario, IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<MyContext>();


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Usuario/Login";
                options.AccessDeniedPath = "/Usuario/AcessaNegado";
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "MeuCookieAuthentication";
                options.LoginPath = "/Usuario/Login"; // Rota para a página de login
                options.AccessDeniedPath = "/Usuario/Login"; // Rota para a página de acesso negado
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "aluno",
                pattern: "Aluno/{action=Index}/{id?}",
                defaults: new { controller = "Aluno" });
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //
            GerarDadosDB.IncluiDadosDB(app);

            // Aula 7
            app.UseAuthentication();
        }
    }
}
