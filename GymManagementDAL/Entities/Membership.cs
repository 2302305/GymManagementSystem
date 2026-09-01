namespace GymManagementDAL.Entities
{
    public class Membership : BaseEntity
    {
        //Each membership supposed to be to one person and only one
        public Member Member { get; set; } = null!;
        public int MemberId { get; set; }
        public Plan Plan { get; set; } = null!;
        public int PlanId { get; set; }
        //StartDate will be the createdAt

        public DateTime EndDate { get; set; }
        public string Status
        {
            get
            {
                if (EndDate >= DateTime.Now)
                    return "Expired";
                else
                    return "Active";
            }
        }

    }
}
