using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bakery.Core.ModelBuilders
{
    public class ModelBuilderBase<T> where T:class
    {
        public ModelBuilderBase(EntityTypeBuilder<T> entity)
        {
            
        }
    }
}