using GymManagementDAL.Entities;

namespace GymManagementDAL.Context.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.Property(p => p.Description)
                 .HasColumnType("VarChar")
                 .HasMaxLength(50);

            builder.ToTable(Tb =>
            Tb.HasCheckConstraint("SessionCapacityCheck", "Capacity Between 1 and 25 "));
            builder.ToTable(Tb => Tb.HasCheckConstraint(
                "SessionEndDateCheck",
                "StartDate < EndDate"
            ));
            //SessionCategoryRelation 
            builder.HasOne(Rl => Rl.SessionCategory)
                .WithMany(Rl => Rl.Sessions)
                .HasForeignKey(c => c.CategoryId);
            //SessionTrainerRelation 
            builder.HasOne(Rl => Rl.TrainerSession)
                .WithMany(Rl => Rl.Sessions)
                .HasForeignKey(c => c.TrainerId);

        }
    }
}
