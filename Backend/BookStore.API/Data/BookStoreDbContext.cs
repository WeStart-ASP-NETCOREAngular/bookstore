using BookStore.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data
{
    public class BookStoreDbContext : IdentityDbContext<AppUser>
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Name = "User", NormalizedName = "USER" });

            base.OnModelCreating(builder);
        }

        public DbSet<Publisher> Publishers { get; set; }
    }
}
