using Bakery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bakery.Core.ModelBuilders
{
    public class CustomerAddressModelBuilder : ModelBuilderBase<CustomerAddress>
    {
        public CustomerAddressModelBuilder(EntityTypeBuilder<CustomerAddress> entity) : base(entity)
        {
            entity.HasKey(e => new { e.CustomerId, e.AddressId })
                .HasName("PRIMARY");

            entity.HasIndex(e => e.AddressId)
                .HasName("AddressId_UNIQUE")
                .IsUnique();

            entity.Property(e => e.CustomerId).HasColumnType("int(11)");

            entity.Property(e => e.AddressId)
                .HasColumnType("int(11)")
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