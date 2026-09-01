namespace GymManagementDAL.Entities
{
    public class Member : GymUser
    {
        //JoinDate== CreatedAt =>Rename using fluentApi
        public string Photo { get; set; } = null!;

        //Member - HealthRecord Relation
        public HealthRecord HealthRecord { get; set; } = null!; // required
        //Each member could have multiple memberships 
        public ICollection<Membership> Memberships { get; set; } = null!;
        //Each member Could have multiple sessions
        public ICollection<MemberSessionRL> MemberSession { get; set; } = null!;

    }
}
