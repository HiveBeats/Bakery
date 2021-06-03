using Bakery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bakery.Core.ModelBuilders
{
    public class CustomerAddressModelBuilder : ModelBuilderBase<CustomerAddress>
    {
        public CustomerAddressModelBuilder(EntityTypeBuilder<CustomerAddress> entity) : base(entity)
        {
            entity.HasIndex(e => new { e.CustomerId, e.AddressId })
                .HasName("Customer_AddressId_UNIQUE").IsUnique();

            entity.HasKey(e => e.AddressId)
                .HasName("PRIMARY");

            entity.Property(e => e.CustomerId);

            entity.Property(e => e.AddressId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.AddressName).HasMaxLength(255);

            entity.Property(e => e.Latitude).HasColumnType("float(10,6)");

            entity.Property(e => e.Longitude).HasColumnType("float(10,6)");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.CustomerAddress)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUSTOMER_ADDRESS_CUSTOMER");

            entity.HasIndex(x => x.DateEnd);
        }
    }
}