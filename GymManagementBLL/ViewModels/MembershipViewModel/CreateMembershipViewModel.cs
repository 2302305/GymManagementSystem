using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace GymManagementBLL.ViewModels.MembershipViewModel
{
    public class CreateMembershipViewModel
    {
        [Required(ErrorMessage = "Member is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Member.")]
        [DisplayName("Member")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Plan is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Plan.")]
        [DisplayName("Plan")]
        public int PlanId { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        // Populated by the controller — not submitted by the form
        public IEnumerable<SelectListItem> Members { get; set; } = [];
        public IEnumerable<SelectListItem> Plans { get; set; } = [];
    }
}