using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class ScheduleController(IScheduleService scheduleService) : Controller
    {
        public IActionResult Index()
        {
            var schedule = scheduleService.GetScheduleIndex();
            return View(schedule);
        }
    }
}