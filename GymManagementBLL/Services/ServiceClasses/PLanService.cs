using GymManagementBLL.ViewModels.PlanViewModel;

namespace GymManagementBLL.Services.ServiceClasses
{
    public class PLanService(IUnitOfWork unitOfWork) : IPlanServices
    {
        public IEnumerable<PlanViewModel>? GetAllPlans()
        {
            var Plans = unitOfWork.GetRepository<Plan>().GetAll();
            if (Plans == null || !Plans.Any())
                return [];
            //return (IEnumerable<PlanViewModel>)Plans;
            return Plans.Select(p => new PlanViewModel()
            {
                Id = p.Id,
                Description = p.Description,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive,
                Name = p.Name,
                Price = p.Price
            });

        }

        public PlanViewModel? GetPlanById(int PlanId)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan == null) return null;
            return new PlanViewModel()
            {
                Id = Plan.Id,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                IsActive = Plan.IsActive,
                Name = Plan.Name,
                Price = Plan.Price
            };

        }

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan == null || ActiveCheck(PlanId)) return false;
            try
            {

                //Tuple
                (Plan.Description, Plan.DurationDays, Plan.Price, Plan.UpdatedAt) =
                    (UpdatedPlan.Description, UpdatedPlan.DurationDays, UpdatedPlan.Price, DateTime.Now);
                unitOfWork.GetRepository<Plan>().Update(Plan);

                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;

            }
        }


        public UpdatePlanViewModel? UpdatePlanView(int PlanId)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan == null || Plan.IsActive == false || ActiveCheck(PlanId)) return null;
            return new UpdatePlanViewModel()
            {
                Price = Plan.Price,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                PlanName = Plan.Name,
            };

        }
        #region HelperMethods
        private bool ActiveCheck(int planId)
        {
            var ActiveMemberShip = unitOfWork.GetRepository<Membership>().GetAll(s => s.PlanId == planId && s.Status == "Active");
            return ActiveMemberShip.Any();
        }
        #endregion
        //Soft Delete // Update
        public bool TogglePlanActivation(int PlanId)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan is null || ActiveCheck(PlanId)) return false;
            Plan.IsActive = Plan.IsActive == true ? false : true;
            Plan.UpdatedAt = DateTime.Now;
            try
            {
                unitOfWork.GetRepository<Plan>().Update(Plan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
