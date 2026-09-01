using GymManagementDAL.Entities;

namespace GymManagementDAL.Context.Configurations
{
    internal class MemberConfiguration : GymUserConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(p => p.CreatedAt)
               .HasColumnName("JoinDate")
               .HasDefaultValueSql("GETDATE()");
            base.Configure(builder);

        }
    }
}
