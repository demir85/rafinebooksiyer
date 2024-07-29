using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Eticaret12.ViewModels;
using Eticaret12.Models;
using Microsoft.AspNetCore.Identity;

namespace Eticaret12.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // public DbSet<Eticaret12.ViewModels.UserRole> UserRole { get; set; } = default!;
        //public DbSet<Eticaret12.Models.UserRol> UserRol { get; set; } = default!;

        public DbSet<IdentityUserRoley> UserRolesY { get; set; }
		public DbSet<UserPasswordHistory> UserPasswordHistories { get; set; }
       // public DbSet<Eticaret12.Models.UserPasswordHistory> UserPasswordHistory { get; set; } = default!;
		// public DbSet<UserPasswordHistory> UserPasswordHistories { get; set; }
      
	}
}
