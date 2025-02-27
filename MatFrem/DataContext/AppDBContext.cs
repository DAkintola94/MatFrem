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
    public class AppDBContext : IdentityDbContext<ApplicationUser, IdentityRole, string> //IdentityDbContext is a class that is used to interact with the database,
                                                                                         //it is a part of the Identity framework
                                                                                         //similar to dbContext, but with added functionality for Identity and user management
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<ShopModel> Shop_detail { get; set; }
        public DbSet<ProductModel> Product_detail { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<AdviceModel> Advice { get; set; }
        public DbSet<ShoppingCartModel> ShoppingCart { get; set; }
        public DbSet<OrderStatus> OrderState { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CategoryModel> Category { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var sysAdminRoleId = "1";
            var driverRoleId = "2";
            var customerRoleId = "3";

            //Seed roles for (User, Admin Superadmin)
            // Seed superAdmin
            // Add all the role to the superadmin

            var roles = new List<IdentityRole> //List of roles, stacked in IdenityRole list
            {

               new IdentityRole //role 1
               {
                  Name = "System Administrator",
                  NormalizedName = "SYSADMIN",
                  Id = sysAdminRoleId,
                  ConcurrencyStamp = sysAdminRoleId
               },

               new IdentityRole //role 2
               {
                   Name = "Driver",
                   NormalizedName = "DRIVER",
                   Id = driverRoleId,
                   ConcurrencyStamp = driverRoleId
               },

               new IdentityRole //role 3 
               {
                   Name = "Customer",
                   NormalizedName = "CUSTOMER",
                   Id = customerRoleId,
                   ConcurrencyStamp = customerRoleId
               },

               };

            modelBuilder.Entity<IdentityRole>().HasData(roles); //Seed data for roles

            //Seed data for users

            var sysadminId = "1";
            var sysAdminUser = new ApplicationUser
            {
                Id = sysadminId,
                UserName = "sysadmin@test.com",
                NormalizedUserName = "sysadmin@test.com".ToUpper(),
                Email = "sysadmin@test.com",
                NormalizedEmail = "sysadmin@test.com".ToUpper(),
                FirstName = "System",
                LastName = "Administrator",
                PhoneNumber = "40748608"
            };

            sysAdminUser.PasswordHash = new PasswordHasher<ApplicationUser> //Hashing password for sysAdminUser
                ().HashPassword(sysAdminUser, "Testingtesting1234");

            modelBuilder.Entity<ApplicationUser>().HasData(sysAdminUser); //using applicationuser because its custom model than add more, 
            //its possible because it inherits from IdentityUser, the is the default

            var sysadminRoles = new List<IdentityUserRole<string>> //List of roles for sysAdminUser
            {
                new IdentityUserRole<string> //Adding role so sysAdminUser can be a system administrator
                {
                    RoleId = sysAdminRoleId,
                    UserId = sysadminId
                },

                new IdentityUserRole<string> //Adding role so sysAdminUser can be a driver
                {
                    RoleId = driverRoleId,
                    UserId = sysadminId
                },

                new IdentityUserRole<string> //Adding role so sysAdminUser can be a customer
                {
                    RoleId = customerRoleId,
                    UserId = sysadminId
                }

            };


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(sysadminRoles); //Seed data for sysAdminUser roles

            //Seeding data for driverUser

            var driverId = "2";
            var driverUser = new ApplicationUser //Creating a driver user
            {
                Id = driverId, //All this is possible because of IdentityUser
                UserName = "driver@test.com",
                NormalizedUserName = "DRIVER@TEST.COM",
                Email = "driver@test.com", //All this is possible because of IdentityUser
                NormalizedEmail = "DRIVER@TEST.COM",
                FirstName = "Test",
                LastName = "Driver",
                PhoneNumber = "95534356"
            };

            driverUser.PasswordHash = new PasswordHasher<ApplicationUser> //Hashing password for driverUser, linked to ApplicationUser
                ().HashPassword(driverUser, "Drivertesting1234");

            modelBuilder.Entity<ApplicationUser>().HasData(driverUser); //Seed data for driverUser, linked to ApplicationUser

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = driverRoleId,
                    UserId = driverId
                }

              );

            //Seeding data for customerUser

            var customerId = "3";
            var customerUser = new ApplicationUser //Creating a customer user
            {
                Id = customerId, //All this is possible because of IdentityUser
                UserName = "customer@test.com",
                NormalizedUserName = "CUSTOMER@TEST.COM", //All this is possible because of IdentityUser
                Email = "customer@test.com",
                NormalizedEmail = "CUSTOMER@TEST.COM",
                FirstName = "Test",
                LastName = "Customer",
                PhoneNumber = "43342364"
            };

            customerUser.PasswordHash = new PasswordHasher<ApplicationUser> //Hashing password for customerUser, linked to ApplicationUser
                ().HashPassword(customerUser, "Customertesting1234");

            modelBuilder.Entity<ApplicationUser>().HasData(customerUser); //Seed data for customerUser, linked to ApplicationUser

            modelBuilder.Entity<IdentityUserRole<string>>().HasData( //Seed data for customerUser roles
                new IdentityUserRole<string>
                {
                    RoleId = customerRoleId,
                    UserId = customerId
                }
              );

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

            modelBuilder.Entity<CategoryModel>()
             .HasKey(oi => oi.CategoryId);


            //Relationships, remember to think of .Entity as THIS => table in the database
            //we are defining the relationships between the tables here, by foreign keys

            modelBuilder.Entity<OrderModel>()
                .HasMany(o => o.OrderItems)
                .WithOne() //When nothing is specified like this, the line means with many OrderModel(the class attached above)
				.HasForeignKey(oi => oi.OrderModelId);

            modelBuilder.Entity<OrderModel>()
                .HasMany(o => o.Product)
                .WithMany();

            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.OrderStatus)
                .WithMany()
                .HasForeignKey(o => o.OrderStatusID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.CategoryModel)
                .WithMany()
                .HasForeignKey(o => o.CategoryM_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductModel>()
                .HasOne(c => c.CategoryModel)
                .WithMany()
                .HasForeignKey(ci => ci.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            //Seeding data into OrderStatus table in the database
            modelBuilder.Entity<OrderStatus>().HasData(
               new OrderStatus { OrderStatusID = 1, StatusDescription = "Motatt" },
               new OrderStatus { OrderStatusID = 2, StatusDescription = "Under behandling" },
               new OrderStatus { OrderStatusID = 3, StatusDescription = "På vei" },
                new OrderStatus { OrderStatusID = 4, StatusDescription = "Levert" },
               new OrderStatus { OrderStatusID = 5, StatusDescription = "Order kansellert" }
           );

            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { CategoryId = 1, CategoryName = "Meleri & Egg" },
                new CategoryModel { CategoryId = 2, CategoryName = "Frukt & Grønt" },
                new CategoryModel { CategoryId = 3, CategoryName = "Kjøtt & Fisk" },
                new CategoryModel { CategoryId = 4, CategoryName = "Brød & Bakervarer" },
                new CategoryModel { CategoryId = 5, CategoryName = "Drikkevarer" },
                new CategoryModel { CategoryId = 6, CategoryName = "Søtsaker" },
                new CategoryModel { CategoryId = 7, CategoryName = "Tørrvarer" },
                new CategoryModel { CategoryId = 8, CategoryName = "Husholdning" },
                new CategoryModel { CategoryId = 9, CategoryName = "Personlig pleie" },
                new CategoryModel { CategoryId = 10, CategoryName = "Annet" }
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
