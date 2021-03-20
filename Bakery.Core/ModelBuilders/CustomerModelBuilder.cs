using Bakery.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bakery.Core.ModelBuilders
{
    public class CustomerModelBuilder : ModelBuilderBase<Customer>
    {
        public CustomerModelBuilder(EntityTypeBuilder<Customer> entity) : base(entity)
        {
            entity.HasIndex(e => e.CustomerName)
                .HasName("IDX_CUSTOMER_NAME");

            entity.Property(e => e.CustomerId).HasColumnType("int(11)");

            entity.Property(e => e.CustomerName)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}