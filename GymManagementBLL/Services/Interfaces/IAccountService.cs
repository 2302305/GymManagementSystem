using GymManagementBLL.ViewModels.AccountViewModel;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IAccountService
    {
        ApplicationUser? ValidateUser(LoginViewModel loginViewModel);
    }
}
