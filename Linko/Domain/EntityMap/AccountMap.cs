using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Linko.Domain
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.AccountName).HasMaxLength(25).IsRequired();
            builder.Property(x => x.AccountType).HasMaxLength(25).IsRequired();
            builder.Property(x => x.Prefix).HasMaxLength(25).IsRequired();
            builder.Property(x => x.Link).HasMaxLength(25).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.InsertDate).HasDefaultValue(DateTime.Now); 
            builder.Property(x => x.UpdateDate).HasDefaultValue(null);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.Property(x => x.DeleteDate).HasDefaultValue(null);
            builder.Property(x => x.Version).IsRowVersion();
        }
    }
}
