using GymManagementDAL.Entities;

namespace GymManagementDAL.Context.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.CategoryName)
                .HasColumnType("VarChar")
                .HasMaxLength(20);
        }
    }
}
