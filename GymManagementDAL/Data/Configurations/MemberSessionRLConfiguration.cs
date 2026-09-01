using GymManagementDAL.Entities;

namespace GymManagementDAL.Context.Configurations
{
    public class MemberSessionRLConfiguration : IEntityTypeConfiguration<MemberSessionRL>
    {
        public void Configure(EntityTypeBuilder<MemberSessionRL> builder)
        {
            builder.HasKey(K => new { K.MemberId, K.SessionId });
            builder.Ignore(I => I.Id);
            builder.Property(p => p.CreatedAt)
                .HasColumnName("BookingDate")
                .HasDefaultValueSql("GetDate()");
        }
    }
}
