using Chatter.Domain.Categories;
using Chatter.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatter.Infra.Data.Mappings
{
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public override void Map(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.Description)
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Image)
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Removed)
                .HasDefaultValue(false);

            builder.Property(c => c.Created)
                .HasDefaultValueSql("GETDATE()");
            
            builder.Ignore(c => c.Topics);
            
            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("Categories");
        }
    }
}