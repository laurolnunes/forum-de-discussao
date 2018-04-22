    using Chatter.Domain.Topics;
using Chatter.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatter.Infra.Data.Mappings
{
    public class TopicMapping : EntityTypeConfiguration<Topic>
    {
        public override void Map(EntityTypeBuilder<Topic> builder)
        {
            builder.Property(t => t.Title)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(t => t.Description)
                .HasColumnType("varchar(1000)");
            
            builder.Property(t => t.Removed)
                .HasDefaultValue(false);

            builder.Property(t => t.Created)
                .HasDefaultValueSql("GETDATE()");

            builder.Ignore(t => t.Category);
            builder.Ignore(t => t.User);
            builder.Ignore(t => t.Posts);

            builder.Ignore(t => t.ValidationResult);
            builder.Ignore(t => t.CascadeMode);

            builder.HasOne(t => t.Category)
                .WithMany(c => c.Topics)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.User)
                .WithMany(u => u.Topics)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Topics");
        }
    }
}