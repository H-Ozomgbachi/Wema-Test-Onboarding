namespace Onboarding.Infrastructure.DataContext.EntityConfiguration
{
    public abstract record BaseEntityConfig<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.CreatedAt).HasColumnName("CREATED_AT");
        }
    }
}
