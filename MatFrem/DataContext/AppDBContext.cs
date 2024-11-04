using MatFrem.Controllers;
using MatFrem.Models.DomainModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MatFrem.DataContext
{
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<ShopModel> Shop_detail { get; set; }
        public DbSet<ProductModel> Product_detail { get; set; }
        public DbSet<CreateAccountModel> Account_creation { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Primary keys, remember to think of .Entity as THIS => table in the database

            modelBuilder.Entity<DriverModel>()
                .HasKey(d => d.DriverID); //Primary key for DriverModel
            modelBuilder.Entity<CustomerModel>()
                .HasKey(c => c.CustomerID); //Primary key for CustomerModel
            modelBuilder.Entity<LocationModel>()
                .HasKey(l => l.LocationID); //Primary key for LocationModel
            modelBuilder.Entity<ProductModel>()
                .HasKey(p => p.ProductID); //Primary key for ProductModel
            modelBuilder.Entity<ShopModel>()
                .HasKey(s => s.ShopID); //Primary key for ShopModel
            modelBuilder.Entity<OrderModel>()
                .HasKey(o => o.OrderID); //Primary key for OrderModel


            //Relationships, remember to think of .Entity as THIS => table in the database

            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.Customer) //Table OrderModel has one Customer
                .WithMany(c => c.Orders) //A Customer can have many orders
                .HasForeignKey(c => c.CustomerID); //Foreign key for CustomerID in OrderModel, primary key for CustomerModel

            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.Driver) //Table OrderModel has one Driver
                .WithMany() //A driver can have many Order
                .HasForeignKey(d => d.DriverID); //Foreign key for DriverID in OrderModel ...

            modelBuilder.Entity<CustomerModel>()
                .HasOne(c => c.Driver) //CustomerModel has one Driver
                .WithOne() //Driver can have one Customer
                .HasForeignKey<CustomerModel>(d => d.DriverID); //need <CustomerModel> because of how one -to-one relationships work in EF Core
            //since we are targeting a specific entity, we need to specify the entity type in the lambda expression

            modelBuilder.Entity<ShopModel>()
                .HasOne(s => s.Location)
                .WithOne()
                .HasForeignKey<ShopModel>(s => s.LocationID);

        }






    }
}
