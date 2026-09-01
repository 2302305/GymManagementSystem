using GymManagementBLL.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GymManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class TrainerController(ITrainerServices trainerServices) : Controller
    {
        public IActionResult Index()
        {
            var Data = trainerServices.GetAllTrainers();
            return View(Data);
        }
        #region Trainer Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel CreatedTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And The Missing Fields");
                return View(nameof(Create), CreatedTrainer);

            }
            bool Result = trainerServices.CreateTrainer(CreatedTrainer);
            if (Result)
            {
                TempData["SuccessMessage"] = "TrainerDetails Created Successfully";

            }
            else
            {
                TempData["ErrorMessage"] = "Error Ocurred While Creating The Member , Check Your Phone number and Email";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region TRainerEdit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant Be A Negative Number or 0";
                return RedirectToAction(nameof(Index));
            }
            var TrainerToUpdate = trainerServices.UpdateTrainerViewModel(id);
            if (TrainerToUpdate is null)
            {
                TempData["ErrorMessage"] = "TrainerDetails Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(TrainerToUpdate);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, TrainerUpdateViewModel trainerUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And The Missing Fields");
                return View("Edit", trainerUpdateViewModel);
            }
            var Result = trainerServices.UpdateTrainer(id, trainerUpdateViewModel);
            if (Result)
            {
                TempData["SuccessMessage"] = "TrainerDetails Updated Successfully";

            }
            else
            {
                TempData["ErrorMessage"] = "Error Ocurred While Updating The TrainerDetails";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region GetTrainerById
        public ActionResult GetTrainerDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant Be A Negative Number or 0";
                return RedirectToAction(nameof(Index));
            }
            var TrainerDetails = trainerServices.GetTrainerById(id);
            if (TrainerDetails == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(TrainerDetails);
        }
        #endregion


        #region Delete Trainer
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant Be A Negative Number or 0";
                return RedirectToAction(nameof(Index));
            }
            var trainer = trainerServices.GetTrainerById(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerName = trainer.Name;
            ViewBag.TrainerId = id;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirm([FromForm] int id)
        {
            var Result = trainerServices.DeleteTrainer(id);
            if (Result)
            {
                TempData["SuccessMessage"] = $"Trainer was deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Delete";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
