using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SadhinBangla.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed Roles(User, Admin, SuperAdmin)
            var adminRoleId = "0617f41c-b3e4-4688-aad5-fbe197b6549d";
            var superAdminRoleId = "64457aeb-0546-4cd6-8ec7-2eabb80e549f";
            var userRoleId = "49e1025e-fe74-4d68-b683-f4c1d3bffad6";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);


            //Seed SuperAdminUser
            var superAdminId = "ea1c3af8-406b-407a-85de-a72002de31d1";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@sadhinbangla.com",
                Email = "superadmin@sadhinbangla.com",
                NormalizedEmail = "superadmin@sadhinbangla.com".ToUpper(),
                NormalizedUserName = "superadmin@sadhinbangla.com".ToUpper(),
                Id = superAdminId
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "superadmin123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);


            // Add All roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }
    }
}
