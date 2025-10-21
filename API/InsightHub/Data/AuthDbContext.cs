using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InsightHub.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "28d65a5b-a7db-4850-b380-83591f7d7531";
            var writerRoleId = "9740f16c-24a1-4224-a7be-1bb00b7c6892";
            var adminUserId = "edc267ec-d43c-4e3b-8108-a1a1f819906d";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                    ConcurrencyStamp = "f2d5b6cc-cc4c-4981-9f8e-11a61fd211a1"
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER",
                    ConcurrencyStamp = "a943b60c-8b7e-44d3-9d76-9c29d3d72e3a"
                }
            );

            var admin = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin@xyz.com",
                NormalizedUserName = "ADMIN@XYZ.COM",
                Email = "admin@xyz.com",
                NormalizedEmail = "ADMIN@XYZ.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEHc7lmP1/7ZK6H4z1xqBl6Uz1Wy7KQOdP3QkM+zvQ4wcB1wJ7ZxFZGlYw3U1h3tuPQ==",
                SecurityStamp = "b547ad6b-cc08-44b9-8bcd-35e9f9042f6e",
                ConcurrencyStamp = "2f60b5b3-4c76-4720-91d1-7c6b89333d22"
            };

            builder.Entity<IdentityUser>().HasData(admin);

            // Give admin both roles
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = readerRoleId },
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = writerRoleId }
            );
        }
    }
}
