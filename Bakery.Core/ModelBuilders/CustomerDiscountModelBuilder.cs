using Bakery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bakery.Core.ModelBuilders
{
    public class CustomerDiscountModelBuilder : ModelBuilderBase<CustomerDiscount>
    {
        public CustomerDiscountModelBuilder(EntityTypeBuilder<CustomerDiscount> entity) : base(entity)
        {
            entity.HasKey(e => new { e.DiscountId, e.CustomerId })
                .HasName("PRIMARY");

            entity.HasIndex(e => e.CustomerId)
                .HasName("FK_DISCOUNT_CUSTOMER_idx");

            entity.HasIndex(e => e.DiscountId)
                .HasName("DiscountId_UNIQUE")
                .IsUnique();

            entity.Property(e => e.DiscountId)
                .HasColumnType("int(11)")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CustomerId).HasColumnType("int(11)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.CustomerDiscount)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DISCOUNT_CUSTOMER");

            entity.HasIndex(x => x.DateEnd);
        }
    }
}