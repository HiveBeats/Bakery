using Bakery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bakery.Core.ModelBuilders
{
    public class CustomerDiscountModelBuilder : ModelBuilderBase<CustomerDiscount>
    {
        public CustomerDiscountModelBuilder(EntityTypeBuilder<CustomerDiscount> entity) : base(entity)
        {
            entity.HasIndex(e => new { e.DiscountId, e.CustomerId })
                .HasName("CustomerId_DiscountId_UNIQUE").IsUnique();

            entity.HasKey(e => e.CustomerId)
                .HasName("FK_DISCOUNT_CUSTOMER_idx");

            entity.HasKey(e => e.DiscountId)
                .HasName("PRIMARY");

            entity.Property(e => e.DiscountId)
                .HasConversion<float>()
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CustomerId)
                .HasConversion<float>();

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