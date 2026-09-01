namespace GymManagementBLL.ViewModels.MemberViewModel
{
    public class HealthRecordViewModel
    {
        [Required(ErrorMessage = "Height is Required")]
        [Range(0.1, 300, ErrorMessage = "Height must be Greater Than 0 and less than 300cm")]
        public decimal Hieght { get; set; }

        [Required(ErrorMessage = "Weight is Required")]
        [Range(0.1, 300, ErrorMessage = "Weight must be Greater Than 0 and less than 300kg")]
        public decimal Weight { get; set; }
        [Required(ErrorMessage = "BloodType is Required")]
        public BloodType BloodType { get; set; }

        [StringLength(500, ErrorMessage = "The Note Must Be At Most 500 Characters")]
        public string? Note { get; set; }

    }
}
