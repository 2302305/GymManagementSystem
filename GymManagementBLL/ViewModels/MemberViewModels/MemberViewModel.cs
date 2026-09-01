namespace GymManagementBLL.ViewModels.MemberViewModel
{
    public class MemberViewModel
    {
        //Get All Members
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Gender { get; set; } = null!;
        //Get Member Details Section nullable 
        public string? PlanName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Address { get; set; }


    }
}
