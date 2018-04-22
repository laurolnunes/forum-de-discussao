using AutoMapper;
using Chatter.Application.AutoMapper;
using Chatter.Infra.CrossCutting.Bus;
using Chatter.Infra.CrossCutting.Identity.Data;
using Chatter.Infra.CrossCutting.Identity.Models;
using Chatter.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Chatter.Infra.CrossCutting.AspNetFilters;
using Microsoft.AspNetCore.Mvc;

namespace Chatter.Site
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("PublicView", policy => policy.RequireClaim("Topic", "Public"));
                options.AddPolicy("LoggedAction", policy => policy.RequireClaim("Topic", "Logged"));
            });

            services.AddMvc(options => { options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalExceptionHandlingFilter))); });
            services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperConfiguration)));
            
            // Registra as injeções de dependência
            RegisterServices(services);

        }

        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              IHttpContextAccessor accessor)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/erro-aplicacao");
                app.UseStatusCodePagesWithReExecute("/erro-aplicacao/{0}");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            // Passa o mecanismo de injeção de dependência para o projeto 
            InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootstrapper.RegisterServices(services);
        }
    }
}
