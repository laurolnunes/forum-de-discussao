using Chatter.Domain.Categories;
using Chatter.Domain.Log;
using Chatter.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatter.Infra.Data.Mappings
{
    public class LogMapping : EntityTypeConfiguration<Log>
    {
        public override void Map(EntityTypeBuilder<Log> builder)
        {
            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Details)
                .HasColumnType("varchar(8000)");

            builder.Property(c => c.Message)
                .HasColumnType("varchar(1000)")
                .IsRequired();

            builder.Property(c => c.Created)
                .HasDefaultValueSql("GETDATE()");
            
            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("Logs");
        }
    }
}