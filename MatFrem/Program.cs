using MatFrem.DataContext;
using MatFrem.Models.DomainModel;
using MatFrem.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MatFrem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IProductRepository, ProductRepository>(); // this line adds the ShoppingRepository to the services collection
            builder.Services.AddScoped<ILocationRepository, LocationRepository>(); // this line adds the LocationRepository to the services collection
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>(); // this line adds the CustomerRepository to the services collection


            builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(11, 5, 2))));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>() //AddIdentity is a method that adds the Identity framework to the services collection
                .AddEntityFrameworkStores<AppDBContext>();


            builder.Services.AddRazorPages();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
