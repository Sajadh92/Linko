using Linko.Helper;
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

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(25).IsRequired();
            builder.Property(x => x.Type).HasMaxLength(25).IsRequired();
            builder.Property(x => x.Link).HasMaxLength(250).IsRequired();
            builder.Property(x => x.ImgUrl).HasMaxLength(300).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(true).IsRequired();
            builder.Property(x => x.InsertDate).HasDefaultValue(Key.DateTimeIQ);
            builder.Property(x => x.UpdateDate).HasDefaultValue(null);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.Property(x => x.DeleteDate).HasDefaultValue(null);
            builder.Property(x => x.Version).IsRowVersion();
        }
    }
}
