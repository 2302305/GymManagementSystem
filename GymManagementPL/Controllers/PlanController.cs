using GymManagementBLL.ViewModels.PlanViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize]

    public class PlanController(IPlanServices pLanService) : Controller
    {
        public ActionResult Index()
        {
            var Plans = pLanService.GetAllPlans();
            return View(Plans);
        }
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant Be A Negative Number or 0";
                return RedirectToAction(nameof(Index));
            }
            var Plan = pLanService.GetPlanById(id);
            if (Plan == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Plan);
        }
        #region Edit
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant Be A Negative Number or 0";
                return RedirectToAction(nameof(Index));
            }
            var Plan = pLanService.UpdatePlanView(id);
            if (Plan == null)
            {
                TempData["ErrorMessage"] = "Plan Cant Be Update";
                return RedirectToAction(nameof(Index));
            }
            return View(Plan);

        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdatePlanViewModel UpdatedPlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Your Credentials");
                return View(UpdatedPlan);
            }
            var Result = pLanService.UpdatePlan(id, UpdatedPlan);
            if (Result)
            {
                TempData["SuccessMessage"] = "Plan Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan Failed To Update";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion
        #region Plan Activation
        [HttpPost]
        public ActionResult Activate([FromRoute] int id)
        {
            var Result = pLanService.TogglePlanActivation(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Plan Status Changed";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan Status Failed To Change Because it Has An Active Membership";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
