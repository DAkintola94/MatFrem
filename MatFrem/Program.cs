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
            builder.Services.AddHttpClient(); // Add HttpClient service

            builder.Services.AddScoped<IProductRepository, ProductRepository>(); // this line adds the ShoppingRepository to the services collection
            builder.Services.AddScoped<IOrderRepository, OrderRepository>(); // this line adds the OrderRepository to the services collection
			builder.Services.AddScoped<IAdviceRepository, AdviceRepository>(); // this line adds the AdviceRepository to the services collection
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddDbContext<AuthDbContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("IdentityConnection"),
            new MySqlServerVersion(new Version(11, 5, 2))));

            builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(11, 5, 2))));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>() //AddIdentity is a method that adds the Identity framework to the services collection
                .AddEntityFrameworkStores<AuthDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
