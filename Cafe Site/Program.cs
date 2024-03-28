using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Cafe_Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<CafeSiteContext>(options => options.UseSqlServer
            (builder.Configuration.GetConnectionString("cs")));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<CafeSiteContext>();
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<CafeSiteContext>();


            builder.Services.AddScoped< IDefaultRepository<Product>, DefaultRepository<Product> >();

            builder.Services.AddScoped< IDefaultRepository<Product_Size_Price>, DefaultRepository<Product_Size_Price> >();

            builder.Services.AddScoped< IProductService, ProductService >();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //   // pattern: "{controller=AdminDashboard}/{action=Index}/{id?}");
            //pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapRazorPages();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "ProductDetails",
                    pattern: "ProductDetails/{id?}",
                    defaults: new { controller = "Home", action = "ProductDetails" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
            app.Run();
        }
    }
}
