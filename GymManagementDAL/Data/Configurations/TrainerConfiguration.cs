using GymManagementDAL.Entities;

namespace GymManagementDAL.Context.Configurations
{
    internal class TrainerConfiguration : GymUserConfiguration<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(p => p.CreatedAt)
                .HasColumnName("HireDate")
                .HasDefaultValueSql("GETDATE()");
            base.Configure(builder);
        }
    }
}
