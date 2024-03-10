
namespace Onboarding.Infrastructure.DataContext.EntityConfiguration
{
    public record CustomerEntityConfig : BaseEntityConfig<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);
            builder.ToTable("CUSTOMERS");
            builder.HasIndex(c => c.PhoneNumber).IsUnique();
            builder.HasIndex(c => c.Email).IsUnique();

            builder.Property(c => c.PhoneNumber).HasColumnName("PHONE_NUMBER").HasMaxLength(20);
            builder.Property(c => c.Email).HasColumnName("EMAIL").HasMaxLength(128);
            builder.Property(c => c.Password).HasColumnName("PASSWORD_HASH");
            builder.Property(c => c.SecurityStamp).HasColumnName("SECURITY_STAMP");
            builder.Property(c => c.StateOfResidence).HasColumnName("STATE_OF_RESIDENCE").HasMaxLength(128);
            builder.Property(c => c.LocalGovernmentArea).HasColumnName("LOCAL_GOVT_AREA").HasMaxLength(128);
        }
    }
}
