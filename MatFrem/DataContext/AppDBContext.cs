using MatFrem.Controllers;
using System.Collections.Generic;
using MatFrem.Models.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MatFrem.Models.ViewModel;
using System.Diagnostics;

namespace MatFrem.DataContext
{
    public class AppDBContext : DbContext 
    {
        public AppDBContext(DbContextOptions<AppDBContext> dbContextopt) : base(dbContextopt)
        {

        }
        public DbSet<ShopModel> Shop_detail { get; set; }
        public DbSet<ProductModel> Product_detail { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<AdviceModel> Advice { get; set; }
        public DbSet<ShoppingCartModel> ShoppingCart { get; set; }
        public DbSet<OrderStatus> OrderState { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Primary keys, remember to think of .Entity as THIS => table in the database

            modelBuilder.Entity<DriverModel>()
                .HasKey(d => d.DriverID); //Primary key for DriverModel

            modelBuilder.Entity<ProductModel>()
                .HasKey(p => p.ProductID); //Primary key for ProductModel

            modelBuilder.Entity<ShopModel>()
                .HasKey(s => s.ShopID); //Primary key for ShopModel

            modelBuilder.Entity<OrderModel>()
                .HasKey(o => o.OrderID); //Primary key for OrderModel

            modelBuilder.Entity<OrderStatus>()
                .HasKey(os => os.OrderStatusID);

            modelBuilder.Entity<AdviceModel>()
                .HasKey(a => a.PostId);

            modelBuilder.Entity<ShoppingCartModel>()
                .HasKey(sc => sc.ShoppingCartID);

            modelBuilder.Entity<OrderItem>()
               .HasKey(oi => oi.Id);

            modelBuilder.Entity<OrderProducts>()
                .HasKey(op => new { op.OrderID, op.ProductID }); //Primary key for OrderProducts, NB! not in use right now

            //Relationships, remember to think of .Entity as THIS => table in the database
            //we are defining the relationships between the tables here, by foreign keys

            modelBuilder.Entity<OrderModel>()
                .HasMany(o => o.OrderItems)
                .WithOne() //When nothing is specified like this, the line means with many OrderModel(the class attached above)
				.HasForeignKey(oi => oi.OrderModelId);

            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.OrderStatus)
                .WithMany()
                .HasForeignKey(o => o.OrderStatusID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductModel>()
                .HasOne(sm => sm.ShopModelO)
                .WithMany()
                .HasForeignKey(smFR => smFR.ShopId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<OrderProducts>() //a somewhat middleman table, that connects OrderModel and ProductModel
            //.HasOne(o => o.OrderM)
            //.WithMany(p => p.OrderProduct) //OrderProduct is in OrderM 
            //.HasForeignKey(op => op.OrderID);

            //modelBuilder.Entity<OrderProducts>() //a somewhat middleman table, that connects OrderModel and ProductModel
            //.HasOne(p => p.ProductM) 
            //.WithMany(p => p.OrderProduct) //OrderProduct is in ProductM
            //.HasForeignKey(pFR => pFR.ProductID);


            //Seeding data into OrderStatus table in the database
            modelBuilder.Entity<OrderStatus>().HasData(
               new OrderStatus { OrderStatusID = 1, StatusDescription = "Motatt" },
               new OrderStatus { OrderStatusID = 2, StatusDescription = "Under behandling" },
               new OrderStatus { OrderStatusID = 3, StatusDescription = "På vei" },
                new OrderStatus { OrderStatusID = 4, StatusDescription = "Levert" },
               new OrderStatus { OrderStatusID = 5, StatusDescription = "Order kansellert" }
           );

            modelBuilder.Entity<ShopModel>().HasData(
                new ShopModel { ShopID = 1, ShopName = "Kiwi" },
                new ShopModel { ShopID = 2, ShopName = "Rema 1000" },
                new ShopModel { ShopID = 3, ShopName = "Coop Mega" },
                new ShopModel { ShopID = 4, ShopName = "Coop Extra"},
                new ShopModel { ShopID = 5, ShopName = "Meny" },
                new ShopModel { ShopID = 6, ShopName = "Bunnpris" },
                new ShopModel { ShopID = 7, ShopName = "Spar"},
                new ShopModel { ShopID = 8, ShopName = "Joker" },
                new ShopModel { ShopID = 9, ShopName = "Obs!" },
                new ShopModel { ShopID = 10, ShopName = "Coop Prix" }
                 );

        }
    }
}
