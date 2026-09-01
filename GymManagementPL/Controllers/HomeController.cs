using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GymManagementPL.Controllers
{
    [Authorize]
    public class HomeController(IAnalyticsService analyticsService) : Controller
    {
        /// <summary>
        /// Action Method Rules
        /// The method must be public.
        ///The method cannot be a static method.
        ///The method cannot be an extension method.
        ///The method cannot be a constructor, getter, or setter.
        ///The method cannot have open generic types.
        ///The method is not a method of the controller base class.
        ///The method cannot contain ref or out parameters.
        /// </summary>
        /// 

        public ActionResult Index()
        {
            var Data = analyticsService.GetAnalyticsData();
            return View(Data);
        }
    }
}
