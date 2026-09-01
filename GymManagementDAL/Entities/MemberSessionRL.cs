namespace GymManagementDAL.Entities
{
    public class MemberSessionRL : BaseEntity
    {
        public int MemberId { get; set; }
        public int SessionId { get; set; }

        public Member Member { get; set; } = null!;

        public Session Session { get; set; } = null!;

        //Booking Day Craeted At 
        public bool IsAttended { get; set; }

    }
}