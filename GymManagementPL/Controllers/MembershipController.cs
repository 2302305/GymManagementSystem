using GymManagementBLL.ViewModels.MembershipViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MembershipController(IMembershipService _membershipService) : Controller
    {
        public ActionResult Index()
        {
            var Memberships = _membershipService.GetAll();
            return View(Memberships);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = _membershipService.GetCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMembershipViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var freshVm = _membershipService.GetCreateViewModel();
                viewModel.Members = freshVm.Members;
                viewModel.Plans = freshVm.Plans;
                return View(viewModel);
            }

            // ✅ Check for duplicate before calling Create
            bool isDuplicate = _membershipService.IsDuplicate(viewModel.MemberId, viewModel.PlanId);
            if (isDuplicate)
            {
                ModelState.AddModelError(string.Empty,
                    "This member is already enrolled in the selected plan.");

                var freshVm = _membershipService.GetCreateViewModel();
                viewModel.Members = freshVm.Members;
                viewModel.Plans = freshVm.Plans;
                return View(viewModel);
            }

            bool success = _membershipService.Create(viewModel);
            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to create membership. Check dates and try again.";
                var freshVm = _membershipService.GetCreateViewModel();
                viewModel.Members = freshVm.Members;
                viewModel.Plans = freshVm.Plans;
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int memberId, int planId)
        {
            bool success = _membershipService.Cancel(memberId, planId);

            if (!success)
                TempData["ErrorMessage"] = "Failed to cancel membership. Please try again.";
            else
                TempData["SuccessMessage"] = "Membership cancelled successfully.";

            return RedirectToAction("Index");
        }
    }
}