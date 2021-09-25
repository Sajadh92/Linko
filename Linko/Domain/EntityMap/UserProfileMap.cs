using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Linko.Domain
{
    public class UserProfileMap : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfile", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Username).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(35).IsRequired();
            builder.Property(x => x.AccountNo).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.FullName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ProfileImg).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Lang).HasMaxLength(2).IsRequired();
            builder.Property(x => x.Bio).HasMaxLength(255).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.LastAccessDate).HasDefaultValue(DateTime.Now).IsRequired();
            builder.Property(x => x.DeleteAfterPeriod).HasDefaultValue(12).IsRequired();
            builder.Property(x => x.InsertDate).HasDefaultValue(DateTime.Now); 
            builder.Property(x => x.UpdateDate).HasDefaultValue(null);
            builder.Property(x => x.DeleteDate).HasDefaultValue(null); 
            builder.Property(x => x.Version).IsRowVersion();

            builder.Ignore(x => x.Accounts);
        }
    }
}
