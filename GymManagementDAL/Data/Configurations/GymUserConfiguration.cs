namespace GymManagementDAL.Context.Configurations
{
    public class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {

            #region Name
            //Name///////////////////
            builder.Property(p => p.Name)
                .HasColumnType("varChar")
                .HasMaxLength(50);
            #endregion
            #region AddressConfig 
            //Less Efficient//////////////////////////////////////////
            //                                                      /
            ////City//////////////////                              /
            //builder.Property(p => p.Address.City)                 /
            //    .HasColumnType("varChar")                         /
            //    .HasMaxLength(30);                                /
            ////Street////////////////                              /   
            //builder.Property(p => p.Address.Street)               /
            //    .HasColumnType("varChar")                         /   
            //  .HasMaxLength(30);                                  /
            /////////////////////////////////////////////////////////


            //City And Street From Owned Class

            builder.OwnsOne(o => o.Address, AddressBuilder =>
            {
                AddressBuilder.Property(p => p.Street)
                .HasColumnType("varChar")
                .HasColumnName("Street")
                .HasMaxLength(30);
                AddressBuilder.Property(p => p.City)
                .HasColumnType("varChar")
                .HasColumnName("City")
                .HasMaxLength(30);
                AddressBuilder.Property(p => p.BuildingNumber)
                .HasColumnName("BuildingNumber");

            });
            #endregion
            #region EmailConfig
            //Email//////////////////
            builder.Property(p => p.Email)
                .HasMaxLength(100)
                .HasColumnType("varChar");
            builder.HasIndex(i => i.Email).IsUnique();
            //Email Format
            builder.ToTable(TB => TB.HasCheckConstraint("Email",
"Email LIKE '%@%.%'"));
            #endregion
            #region PhoneConfig
            //Phone//////////////////

            builder.Property(p => p.Phone)
                .HasMaxLength(11)
                .IsRequired(true);
            //Unique Phone
            builder.HasIndex(i => i.Phone).IsUnique();
            //Phone Format
            builder.ToTable(TB => TB.HasCheckConstraint("CK_GymUser_Phone",
            "Phone LIKE '01%' AND Phone Not Like '%[^0-9]%' "));
            #endregion
        }
    }
}
