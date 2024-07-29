using Eticaret12.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Eticaret12.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Eticaret12.Data
{
    public class EticaretVeritabaniContext:DbContext
    {
        public EticaretVeritabaniContext(DbContextOptions<EticaretVeritabaniContext> options)
           : base(options)
        {
        }
        public DbSet<Album>Albums { get; set; } 
        //public DbSet<Eticaret12.ViewModels.UserRole> UserRole { get; set; } = default!;
       public DbSet<UserRol> userRols { get; set; }
       public DbSet<Tur> Turss { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<UserPaymentCodeHistory> UserPaymentCodeHistories { get; set; }

        public DbSet<OrderNumberHistory> OrderNumberHistories { get; set;}
        public DbSet<CreditCart> CreditCarts { get; set; }

    

    /* protected virtual void OnModelCreating(DbModelBuilder)
    {
        ModelBuilder.Entity<Tür>().HasNoKey();
    } */
}
}
