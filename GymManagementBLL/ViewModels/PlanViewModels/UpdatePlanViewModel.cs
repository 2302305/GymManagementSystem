namespace GymManagementBLL.ViewModels.PlanViewModel
{
    public class UpdatePlanViewModel
    {
        public string PlanName { get; set; } = null!;
        [Required(ErrorMessage = "The Description Is Required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "the description must be less than 100")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "The Price Is Required")]
        [Range(0.1, 10000, ErrorMessage = "the Range is from 0.1 to 10000")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Duration Days Is Required")]
        [Range(0, 365, ErrorMessage = "the Range is from 1 to 365")]
        public int DurationDays { get; set; }
    }
}
