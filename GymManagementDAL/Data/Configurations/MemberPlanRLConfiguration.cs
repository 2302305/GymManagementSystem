using GymManagementDAL.Entities;

namespace GymManagementDAL.Context.Configurations
{
    public class MemberPlanRLConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(p => p.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GetDate()");

            builder.HasKey(p => new { p.PlanId, p.MemberId });
            builder.Ignore(p => p.Id);


        }
    }
}
