using GymManagementBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    [Authorize]

    public class SessionController(ISessionServices sessionService) : Controller
    {

        // GET: SessionController
        public ActionResult Index()
        {
            var Sessions = sessionService.GetAllSessions();
            return View(Sessions);
        }
        public ActionResult Schedule()
        {
            var sessions = sessionService.GetAllSessions();
            return View(sessions);
        }

        // GET: SessionController/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id can't be 0 or negative.";
                return RedirectToAction(nameof(Index));
            }

            var session = sessionService.GetSessionDetails(id); // ✅ use new method
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(session);
        }

        // GET: SessionController/Create
        public ActionResult Create()
        {
            LoadDropDownsForCategories();
            LoadDropDownsForTrainers();
            return View();
        }

        // POST: SessionController/Create
        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createdSession)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadDropDownsForCategories();
                    LoadDropDownsForTrainers();
                    ModelState.AddModelError("DataInvalid", "Check Data And The Missing Fields");
                    return View(nameof(Create), createdSession);
                }
                var Result = sessionService.CreateSession(createdSession);
                if (Result)
                {
                    TempData["SuccessMessage"] = "Session Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Session Failed To be Created ";
                    LoadDropDownsForCategories();
                    LoadDropDownsForTrainers();
                    return View(createdSession);
                }
            }
            catch
            {
                return View();
            }
        }
        #region HelperMethods
        private void LoadDropDownsForTrainers()
        {
            var Trainers = sessionService.GetTrainersForDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "TrainerId", "TrainerName");
        }
        private void LoadDropDownsForCategories()
        {
            var Categories = sessionService.GetCategoriesForDropDown();
            ViewBag.Categories = new SelectList(Categories, "CategoryId", "CategoryName");
        }
        #endregion
        // GET: SessionController/Edit/5
        public ActionResult Edit(int id)
        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant Be Less than or = 0";
                return RedirectToAction(nameof(Index));
            }
            var session = sessionService.UpdateSessionViewModel(id);

            if (session == null)
            {
                TempData["ErrorMessage"] = "Only Future Sessions With No Bookings is Able to be Updated";
                return RedirectToAction(nameof(Index));
            }
            LoadDropDownsForTrainers();

            return View(session);
        }

        // POST: SessionController/Edit/5
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdateSessionViewModel updateSession)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    LoadDropDownsForTrainers();
                    ModelState.AddModelError("DataInvalid", "Check Data And The Missing Fields");
                    return View(updateSession);
                }
                var Result = sessionService.UpdateSession(updateSession, id);
                if (Result)
                {
                    TempData["SuccessMessage"] = "Session Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Session Failed To be Updated ";
                    LoadDropDownsForTrainers();
                    return View(updateSession);
                }
            }
            catch
            {

                TempData["ErrorMessage"] = "Session Failed To be Updated ";
                LoadDropDownsForTrainers();
                return View(updateSession);
            }
        }

        // GET: SessionController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant Be Less than or = 0";
                return RedirectToAction(nameof(Index));
            }
            var Result = sessionService.GetSessionById(id);
            if (Result == null)
            {
                TempData["ErrorMessage"] = "Failed To Delete Session Not Found Session";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = id;
            return View();

        }

        // POST: SessionController/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed([FromRoute] int id)
        {
            try
            {
                var Result = sessionService.DeleteSession(id);
                if (Result)
                    TempData["SuccessMessage"] = "Session Deleted Successfully";

                else
                    TempData["ErrorMessage"] = "Failed To Delete Session";

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
                ;
            }
        }
    }
}
