using System;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductCatalogUI.Infrastructure.HttpMessageHandlers;
using ProductCatalogUI.Services;

namespace ProductCatalogUI
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
            services.AddControllersWithViews();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = true;
            });

            services.AddTransient<AcceptHeaderHttpMessageHandler>();
            services.AddTransient<EnsureSuccessHttpMessageHandler>();

            services.AddHttpClient<IProductCatalogService, ProductCatalogService>(c =>
            {
                c.BaseAddress = new Uri(Configuration.GetSection("ProductCatalogAPI")["BaseUrl"]);
            }).AddHttpMessageHandler<AcceptHeaderHttpMessageHandler>()
              .AddHttpMessageHandler<EnsureSuccessHttpMessageHandler>();
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
                //The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.CurrencySymbol = "€";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ProductCatalog}/{action=Index}/{id?}");
            });
        }
    }
}
