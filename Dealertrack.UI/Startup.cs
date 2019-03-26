using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dealertrack.Sales.Application.Business;
using Dealertrack.Sales.Application.Interfaces;
using Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate;
using Dealertrack.Sales.Infrastructure;
using Dealertrack.Sales.Infrastructure.Converters;
using Dealertrack.Sales.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dealertrack.UI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<SalesContext>(options =>
            {
                options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), providerOptions => {
                        providerOptions.CommandTimeout(60);
                        providerOptions.MigrationsAssembly("Dealertrack.UI");
                     })
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddTransient<ISalesBusiness, SalesBusiness>();
            services.AddTransient<CSVConverter, CSVConverter>();

            services.AddMvc( options => {
                options.MaxModelValidationErrors = 50;
                options.AllowValidatingTopLevelNodes = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Sales}/{action=Index}/{id?}");
            });
        }
    }
}
