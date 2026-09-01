using GymManagementBLL.ViewModels.ScheduleViewModels;

namespace GymManagementBLL.Services.ServiceClasses
{
    public class ScheduleService(ISessionServices sessionService) : IScheduleService
    {
        // ✅ Reuses GetAllSessions — no duplicate logic
        private IEnumerable<SessionViewModel> GetAll()
            => sessionService.GetAllSessions();

        public IEnumerable<SessionViewModel> GetOngoing()
            => GetAll().Where(s => s.Status == "OnGoing");

        public IEnumerable<SessionViewModel> GetUpcoming()
            => GetAll().Where(s => s.Status == "UpComing");

        public IEnumerable<SessionViewModel> GetCompleted()
            => GetAll().Where(s => s.Status == "Completed");
        public ScheduleIndexViewModel GetScheduleIndex()
        {
            return new ScheduleIndexViewModel
            {
                Ongoing = GetOngoing().ToList(),
                Upcoming = GetUpcoming().ToList(),
                Completed = GetCompleted().ToList()
            };
        }
    }
}