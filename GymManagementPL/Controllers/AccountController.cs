using GymManagementBLL.ViewModels.AccountViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager) : Controller
    {
        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("InvalidLogin", "User Not Found (Check Your Credentials)");
                return View(loginViewModel);
            }
            var User = accountService.ValidateUser(loginViewModel);
            if (User == null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email Or Password");
                return View(loginViewModel);
            }

            var result = signInManager.PasswordSignInAsync(User, loginViewModel.Password, loginViewModel.RememberMe, false).Result;

            if (result.IsNotAllowed)
                ModelState.AddModelError("InvalidLogin", "Your Account is Blocked And Not Allowed");

            if (result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Your Account is Locked");

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View(loginViewModel);


        }
        #endregion
        #region Logout
        [HttpPost]
        public ActionResult Logout()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login", "Account");
        }
        #endregion
        #region AccessDenied
        public ActionResult AccessDenied()
        {
            return View();
        }
        #endregion

    }
}
