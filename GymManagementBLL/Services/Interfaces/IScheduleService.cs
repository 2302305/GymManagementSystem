using GymManagementBLL.ViewModels.ScheduleViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IScheduleService
    {
        IEnumerable<SessionViewModel> GetOngoing();
        IEnumerable<SessionViewModel> GetUpcoming();
        IEnumerable<SessionViewModel> GetCompleted();
        ScheduleIndexViewModel GetScheduleIndex(); // ✅ single call for the controller
    }
}