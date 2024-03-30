using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Site.Models
{
    public class CafeSiteContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Order> Orders { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<Product_Size_Price> Product_Sizes { set; get; }
        public DbSet<Order_Products> Order_Products { set; get; }
        public DbSet<Product_Reviews> Product_Reviews { get; set; }
		public DbSet<Contact> Contact { get; set; }

		public CafeSiteContext() : base() { }
		

		public CafeSiteContext(DbContextOptions<CafeSiteContext> options):base(options){}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=CafeSiteDB;Trusted_Connection=True;TrustServerCertificate=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.user)
                .WithMany(u => u.orders)
                .HasForeignKey(o => o.userId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.user)
                .WithMany(u => u.products)
                .HasForeignKey(o => o.userId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order_Products>()
                .HasOne(o => o.order)
                .WithMany(u => u.Order_Products)
                .HasForeignKey(o => o.Order_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order_Products>()
                .HasOne(p => p.product)
                .WithMany(u => u.Order_Products)
                .HasForeignKey(o => o.Product_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product_Size_Price>()
                .HasOne(p => p.product)
                .WithMany(u => u.Product_Size_Prices)
                .HasForeignKey(o => o.Product_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product_Reviews>()
                .HasOne(p => p.product)
                .WithMany(u => u.Product_Reviews)
                .HasForeignKey(o => o.Product_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order_Products>()
                .HasKey("Product_Id", "Order_Id");

            modelBuilder.Entity<Product_Size_Price>()
                .HasKey("Product_Id", "Size");


            modelBuilder.Entity<Product_Reviews>()
                .HasKey("Product_Id", "User_Name");
        }
    }
}
