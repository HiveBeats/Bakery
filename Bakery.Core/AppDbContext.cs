using Bakery.Core.Entities;
using Bakery.Core.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Core
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
           //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<CustomerDiscount> CustomerDiscount { get; set; }
        public virtual DbSet<DiscountTime> DiscountTime { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Customer>(entity => new CustomerModelBuilder(entity));
            modelBuilder.Entity<CustomerAddress>(entity => new CustomerAddressModelBuilder(entity));
            modelBuilder.Entity<CustomerDiscount>(entity => new CustomerDiscountModelBuilder(entity));
            modelBuilder.Entity<DiscountTime>(entity => new DiscountTimeModelBuilder(entity));
        }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}