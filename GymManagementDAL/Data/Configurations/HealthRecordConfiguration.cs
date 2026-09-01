namespace GymManagementDAL.Context.Configurations
{
    internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            //Relation
            builder.ToTable("Members")
                .HasKey(pk => pk.Id);//Not needed primary Key specification
            builder.HasOne<Member>()
                .WithOne(Rl => Rl.HealthRecord)
                .HasForeignKey<HealthRecord>(Fk => Fk.Id);

            builder.Ignore(I => I.CreatedAt);
            builder.Ignore(I => I.UpdatedAt);
            builder.Property(p => p.Hieght)
                .HasPrecision(5, 2); // e.g. 999.99

            builder.Property(p => p.Weight)
                .HasPrecision(5, 2);


        }
    }
}
