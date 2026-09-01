
using Microsoft.AspNetCore.Http;

namespace GymManagementBLL.ViewModels.MemberViewModel
{
    public class CreateMemberviewModel
    {
        [Required(ErrorMessage = "The Photo is Required")]
        [Display(Name = "Profile Photo")]
        public IFormFile PhotoFile { get; set; }
        [Required(ErrorMessage = "The Name is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name MUST be atleast 3 chracters and at most 50 ")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can only have letters and spaces.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "The Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format ")]//Validation
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Email MUST be atleast 10 chracters and at most 100 ")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone Number is REQUIRED for comunication")]
        [Phone(ErrorMessage = "Invalid Phone number Format")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Must be a valid Egyptian number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "BirthDate is REQUIRED")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is REQUIRED")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Building Number is REQUIRED")]
        [Range(1, 9000, ErrorMessage = "Invalid Building Number")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street is REQUIRED")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 30 characters ")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City is REQUIRED")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City must be between 2 and 30 characters ")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "City can only have letters and spaces.")]

        public string City { get; set; } = null!;
        [Required(ErrorMessage = "Health Record is REQUIRED")]

        public HealthRecordViewModel HealthRecordViewModel { get; set; } = null!;

    }
}
