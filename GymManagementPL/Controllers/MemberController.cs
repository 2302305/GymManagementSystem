using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class MemberController(IMemberServices memberServices) : Controller
    {
        public ActionResult Index()
        {
            var members = memberServices.GetAllMembers();
            return View(members);
        }
        #region Create

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMember(CreateMemberviewModel createdMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And The Missing Fields");
                return View(nameof(Create), createdMember);
            }

            bool NewMember = memberServices.CreateMember(createdMember);
            if (NewMember)
            {
                TempData["SuccessMessage"] = "The Member Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Error Ocurred While Creating The Member , Check Your Phone number and Email";
                return View(nameof(Create), createdMember);
            }


        }
        #endregion
        #region member Update
        [HttpGet]
        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant Be A Negative Number or 0";
                return RedirectToAction(nameof(Index));
            }

            var Member = memberServices.MemberUpdateViewModel(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Member);
        }
        [HttpPost]
        public ActionResult MemberEdit([FromRoute] int id, UpdateMemberViewModel UpdatedMemberView)
        {
            if (!ModelState.IsValid) return View(UpdatedMemberView);

            var Results = memberServices.MemberUpdate(id, UpdatedMemberView);
            if (Results)
            {
                TempData["SuccessMessage"] = "Member Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Be Updated ";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Member Datails and Health 
        public ActionResult MemberDetails(int memberId)
        {
            if (memberId <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant Be A Negative Number or 0";
                return RedirectToAction(nameof(Index));

            }

            var MemberDetails = memberServices.GetMemberDetails(memberId);
            if (MemberDetails == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));

            }
            return View(MemberDetails);
        }
        public ActionResult HealthRecordDetails(int memberId)
        {

            if (memberId <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant Be A Negative Number or 0";
                return RedirectToAction(nameof(Index));

            }
            var memberHealthRecordDetails = memberServices.GetHealthRecordDetails(memberId);
            if (memberHealthRecordDetails == null)
            {
                TempData["ErrorMessage"] = "Member Health Record Is Not Found";
                return RedirectToAction(nameof(Index));

            }
            return View(memberHealthRecordDetails);
        }
        #endregion
        #region Delete
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cant Be A Negative Number or 0";
                return RedirectToAction(nameof(Index));
            }
            var memberCheck = memberServices.GetMemberDetails(id);
            if (memberCheck == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            ViewBag.MemberName = memberCheck.Name;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirm([FromForm] int id)
        {
            var Result = memberServices.DeleteMember(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Deleted Successfully ";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Delete";
            }
            return RedirectToAction(nameof(Index));
        }
    }
    #endregion
}

