using GymManagementBLL.ViewModels.MembershipViewModel;

public interface IMembershipService
{
    IEnumerable<MembershipViewModel> GetAll();
    bool Create(CreateMembershipViewModel viewModel);
    CreateMembershipViewModel GetCreateViewModel();
    bool IsDuplicate(int memberId, int planId);
    bool Cancel(int memberId, int planId); // ✅ add this
}