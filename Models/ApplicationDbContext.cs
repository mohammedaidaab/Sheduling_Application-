using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace WebApplication1.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUsers>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> employees { get; set; } = null!;
        public DbSet<Schedule> schedules { get; set; } = null!;
        public DbSet<MasterData> masterDatas { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			//this.SeedUsers(builder);
			//	this.SeedUserRoles(builder);

		
		
			//seed admin role
			builder.Entity<IdentityRole>().HasData(new IdentityRole
			{
				Name = "Admin",
				NormalizedName = "ADMIN",
				Id = "1",
				ConcurrencyStamp = "1"
			});
            
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "User",
                NormalizedName = "User",
                Id = "2",
                ConcurrencyStamp = "2"
            });

            //create user
            var appUser = new AppUsers
			{
				Id = "1",
				Email = "admin@abc.com",
				EmailConfirmed = true,
				FirstName = "Admin",
				LastName = "Ofoedu",
				UserName = "admin@abc.com",
 
			NormalizedUserName = "admin@abc.com"
			};

			//set user password
			PasswordHasher<AppUsers> ph = new PasswordHasher<AppUsers>();
			appUser.PasswordHash = ph.HashPassword(appUser, "Abc.123456");

			//seed user
			builder.Entity<AppUsers>().HasData(appUser);

			//set user role to admin
			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "1"
            });

            builder.Entity<MasterData>().HasData(
    new MasterData { id = 1, scheduleNo = 1 });

        }






	


	}
	}
