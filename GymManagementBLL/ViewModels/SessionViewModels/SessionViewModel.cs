namespace GymManagementBLL.ViewModels.SessionViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TrainerName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public int AvailableSlots { get; set; }



        #region Computed Properties => calculated properties for the front dev


        public string DisplayDate => $"{StartDate:MMM,dd,yyyy}";
        public string DisplayTimeRange => $"{StartDate:hh:mm tt} - {EndDate:hh:mm tt}";
        public TimeSpan Duration => EndDate - StartDate;
        public string Status
        {
            get
            {
                if (StartDate > DateTime.Now)
                    return "UpComing";
                else if (StartDate <= DateTime.Now && EndDate >= DateTime.Now)
                    return "OnGoing";
                else
                    return "Completed";
            }
        }
        #endregion


    }
}
