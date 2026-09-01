using GymManagementBLL.ViewModels.MembershipViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementBLL.Services.ServiceClasses
{
    public class MembershipService(IUnitOfWork unitOfWork) : IMembershipService
    {
        public IEnumerable<MembershipViewModel> GetAll()
        {
            var memberships = unitOfWork.GetRepository<Membership>()
                                        .GetAllWithIncludes(m => m.Member, m => m.Plan);

            if (memberships is null || !memberships.Any()) return [];

            return memberships.Select(m => new MembershipViewModel
            {
                MemberId = m.MemberId,
                PlanId = m.PlanId,
                StartDate = m.CreatedAt,
                EndDate = m.EndDate,
                MemberName = m.Member.Name,
                PlanName = m.Plan.Name
            });
        }

        public bool Create(CreateMembershipViewModel viewModel)
        {
            if (viewModel is null) return false;
            if (viewModel.StartDate >= viewModel.EndDate) return false;

            // ✅ Check if this Member + Plan combination already exists
            bool alreadyExists = unitOfWork.GetRepository<Membership>()
                                           .GetAll()
                                           .Any(m => m.MemberId == viewModel.MemberId
                                                  && m.PlanId == viewModel.PlanId);

            if (alreadyExists) return false;

            var membership = new Membership
            {
                MemberId = viewModel.MemberId,
                PlanId = viewModel.PlanId,
                CreatedAt = viewModel.StartDate,
                EndDate = viewModel.EndDate,
            };

            unitOfWork.GetRepository<Membership>().Add(membership);
            return unitOfWork.SaveChanges() > 0;
        }

        public CreateMembershipViewModel GetCreateViewModel()
        {
            var members = unitOfWork.GetRepository<Member>().GetAll();
            var plans = unitOfWork.GetRepository<Plan>().GetAll();

            return new CreateMembershipViewModel
            {
                Members = members.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }),
                Plans = plans.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
            };
        }
        public bool IsDuplicate(int memberId, int planId)
        {
            return unitOfWork.GetRepository<Membership>()
                             .GetAll()
                             .Any(m => m.MemberId == memberId
                                    && m.PlanId == planId);
        }
        public bool Cancel(int memberId, int planId)
        {
            var membership = unitOfWork.GetRepository<Membership>()
                                       .GetAll()
                                       .FirstOrDefault(m => m.MemberId == memberId
                                                         && m.PlanId == planId);

            if (membership is null) return false;

            unitOfWork.GetRepository<Membership>().Delete(membership);
            return unitOfWork.SaveChanges() > 0;
        }
    }
}