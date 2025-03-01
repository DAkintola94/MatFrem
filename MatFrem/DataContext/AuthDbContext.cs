using MatFrem.Models.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MatFrem.DataContext
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string> //IdentityDbContext is a class that is used to interact with the database,
                                                                                          //it is a part of the Identity framework
                                                                                          //similar to dbContext, but with added functionality for Identity
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> authDbContextOpts) : base(authDbContextOpts)
        {

        }

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

        }

    }
}
