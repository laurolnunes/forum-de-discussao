using Chatter.Domain.Topics;
using Chatter.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatter.Infra.Data.Mappings
{
    public class PostMapping : EntityTypeConfiguration<Post>
    {
        public override void Map(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Text)
                .HasColumnType("varchar(1000)")
                .IsRequired();

            builder.Property(p => p.Removed)
                .HasDefaultValue(false);

            builder.Property(p => p.Created)
                .HasDefaultValueSql("GETDATE()");

            builder.Ignore(p => p.Topic);
            builder.Ignore(p => p.User);

            builder.Ignore(p => p.ValidationResult);
            builder.Ignore(p => p.CascadeMode);

            builder.HasOne(p => p.Topic)
                .WithMany(t => t.Posts)
                .HasForeignKey(p => p.TopicId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Posts");
        }
    }
}