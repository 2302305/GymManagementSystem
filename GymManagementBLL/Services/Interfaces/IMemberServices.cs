using GymManagementBLL.ViewModels.MemberViewModel;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMemberServices
    {
        bool CreateMember(CreateMemberviewModel createdMember);
        IEnumerable<MemberViewModel> GetAllMembers();
        MemberViewModel? GetMemberDetails(int MemberId);
        HealthRecordViewModel? GetHealthRecordDetails(int MemberId);
        UpdateMemberViewModel? MemberUpdateViewModel(int MemberId);
        bool MemberUpdate(int MemberId, UpdateMemberViewModel updatedMember);
        bool DeleteMember(int MemberId);
    }
}
