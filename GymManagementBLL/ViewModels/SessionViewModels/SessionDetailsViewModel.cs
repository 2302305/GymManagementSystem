namespace GymManagementBLL.ViewModels.SessionViewModels
{
    public class MemberSessionViewModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public DateTime BookedAt { get; set; }
        public bool IsAttended { get; set; }
    }

    // ✅ Use the full type name to avoid namespace/class conflict
    public class SessionDetailsViewModel : SessionViewModel
    {
        public IEnumerable<MemberSessionViewModel> AssignedMembers { get; set; } = [];
        public int AttendedCount => AssignedMembers.Count(m => m.IsAttended);
        public int BookedCount => AssignedMembers.Count();
    }
}