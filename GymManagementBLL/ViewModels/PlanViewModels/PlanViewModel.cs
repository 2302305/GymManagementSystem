namespace GymManagementBLL.ViewModels.PlanViewModel
{
    public class PlanViewModel
    {
        //Get All Plans
        //Get Plan Details
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
