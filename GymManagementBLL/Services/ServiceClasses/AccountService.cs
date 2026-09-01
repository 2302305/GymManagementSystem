using GymManagementBLL.ViewModels.AccountViewModel;
using Microsoft.AspNetCore.Identity;

namespace GymManagementBLL.Services.ServiceClasses
{
    public class AccountService(UserManager<ApplicationUser> userManager) : IAccountService
    {
        public ApplicationUser? ValidateUser(LoginViewModel loginViewModel)
        {

            if (loginViewModel is null)
            {
                return null;
            }
            var User = userManager.FindByEmailAsync(loginViewModel.Email).Result;
            if (User is null) return null;
            var isPasswordValid = userManager.CheckPasswordAsync(User,loginViewModel.Password).Result;
            if(isPasswordValid == false) return null;
            return User;

        }
    }
}
