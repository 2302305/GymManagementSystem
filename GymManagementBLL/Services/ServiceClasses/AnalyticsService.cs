using GymManagementBLL.ViewModels.AnalyticsViewModels;

namespace GymManagementBLL.Services.ServiceClasses
{
    public class AnalyticsService(IUnitOfWork unitOfWork) : IAnalyticsService
    {
        public AnalyticsViewModel GetAnalyticsData()
        {
            var Sessions = unitOfWork.GetRepository<Session>().GetAll();
            return new AnalyticsViewModel()
            {
                ActiveMembers = unitOfWork.GetRepository<Membership>().GetAll(g => g.Status == "Active").Count(),
                TotalMembers = unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = Sessions.Count(c => c.StartDate > DateTime.Now),
                OnGoingSessions = Sessions.Count(c => c.StartDate <= DateTime.Now && c.EndDate >= DateTime.Now),
                CompletedSessions = Sessions.Count(c => c.EndDate < DateTime.Now),

            };
        }
    }
}
