namespace GymManagementBLL.ViewModels.AccountViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="The Email Is Required")]
        [StringLength(50,ErrorMessage ="Your Email must be less than 50 characters")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "The Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }=null!;
        public bool RememberMe { get; set; }
    }
}
