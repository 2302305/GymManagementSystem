namespace GymManagementBLL.ViewModels.ScheduleViewModels
{
    public class ScheduleIndexViewModel
    {
        public IEnumerable<SessionViewModel> Ongoing { get; set; } = [];
        public IEnumerable<SessionViewModel> Upcoming { get; set; } = [];
        public IEnumerable<SessionViewModel> Completed { get; set; } = [];

        // ── Computed ──
        public int TotalOngoing => Ongoing.Count();
        public int TotalUpcoming => Upcoming.Count();
        public int TotalCompleted => Completed.Count();
        public bool HasAnySessions => TotalOngoing + TotalUpcoming + TotalCompleted > 0;
    }
}