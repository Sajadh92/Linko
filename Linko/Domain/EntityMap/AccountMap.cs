using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linko.Domain
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId);
            builder.Property(x => x.AccountName);
            builder.Property(x => x.AccountType);
            builder.Property(x => x.Prefix);
            builder.Property(x => x.Link);
            builder.Property(x => x.InsertDate);
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.DeleteDate);
            builder.Property(x => x.Version).IsRowVersion();
        }
    }
}
