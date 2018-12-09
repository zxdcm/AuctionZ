using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class AuctionContext : IdentityDbContext<User, Role, int> //DbContext
    {
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
        {
            DbInitializer.Seed(this);
        }

        public DbSet<Bid> Bids { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bid>(ConfigureBid);
            modelBuilder.Entity<Lot>(ConfigureLot);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Category>(ConfigureCategory);
            modelBuilder.Entity<Role>(ConfigureRole);
        }

        private void ConfigureBid(EntityTypeBuilder<Bid> builder)
        {
            builder
                .Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
        }

        private void ConfigureLot(EntityTypeBuilder<Lot> builder)
        {
            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Lots)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(x => x.Bids)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .Property(x => x.Money)
                .HasColumnType("decimal(18,2)");
            builder.Ignore(x => x.UserId);

        }

        private void ConfigureCategory(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasMany(x => x.Lots)
                .WithOne(x => x.Category)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.Ignore(x => x.RoleId);
        }

    }
}
