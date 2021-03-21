using Bakery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bakery.Core.ModelBuilders
{
    public class DiscountTimeModelBuilder : ModelBuilderBase<DiscountTime>
    {
        public DiscountTimeModelBuilder(EntityTypeBuilder<DiscountTime> entity) : base(entity)
        {
            entity.HasIndex(e => new { e.TimeId, e.DiscountId })
                .HasName("DiscountId_TimeId_UNIQUE").IsUnique();

            entity.HasIndex(e => e.DiscountId)
                .HasName("FK_DISCOUNT_TIMES_DISCOUNT");

            entity.HasKey(e => e.TimeId)
                .HasName("PRIMARY");

            entity.Property(e => e.TimeId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DiscountId);

            entity.Property(e => e.DayWeek).HasColumnType("int(11)");

            entity.Property(e => e.EndTime).HasColumnType("decimal(5,2)");

            entity.Property(e => e.StartTime).HasColumnType("decimal(5,2)");

            entity.HasOne(d => d.Discount)
                .WithMany(p => p.DiscountTime)
                .HasPrincipalKey(p => p.DiscountId)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DISCOUNT_TIMES_DISCOUNT");
        }
    }
}