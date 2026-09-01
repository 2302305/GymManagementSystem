using GymManagementDAL.Entities;

namespace GymManagementDAL.Context.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("VarChar")
                  .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasColumnType("VarChar")
                .HasMaxLength(200);

            #region PriceConfig
            //builder.Property(p => p.Price)
            //    .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Price)
                .HasPrecision(10, 2);

            #endregion
            #region DurationConfig
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("PlanDurationCheck", "DurationDays BETWEEN 1 AND 365");
            });
            #endregion
        }
    }
}
