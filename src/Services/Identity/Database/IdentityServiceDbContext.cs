using IdentityService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Database
{
    public class IdentityServiceDbContext : IdentityDbContext<User> 
    {
        public IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<User>(entity => entity.ToTable(name: "Users"));

            builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));

            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRoles"));

            builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable(name: "UserClaims"));

            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable(name: "UserTokens"));

            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable(name: "UserLogins"));

            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable(name: "RoleClaims"));

            builder.ApplyConfiguration(new UserConfiguration());

        }
    }
}
