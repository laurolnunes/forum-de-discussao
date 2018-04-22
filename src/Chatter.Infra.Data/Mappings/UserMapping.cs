using Chatter.Domain.Users;
using Chatter.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chatter.Infra.Data.Mappings
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(u => u.IdentityId)
                .HasColumnType("nvarchar(450)")
                .IsRequired();

            builder.Property(u => u.Removed)
                .HasDefaultValue(false);

            builder.Property(u => u.Created)
                .HasDefaultValueSql("GETDATE()");

            builder.Ignore(u => u.Topics);
            builder.Ignore(u => u.Posts);

            builder.Ignore(u => u.ValidationResult);
            builder.Ignore(u => u.CascadeMode);

            builder.ToTable("Users");
        }
    }
}