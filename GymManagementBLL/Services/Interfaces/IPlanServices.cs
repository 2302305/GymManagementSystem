using GymManagementBLL.ViewModels.PlanViewModel;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IPlanServices
    {
        IEnumerable<PlanViewModel>? GetAllPlans();
        PlanViewModel? GetPlanById(int PlanId);

        UpdatePlanViewModel? UpdatePlanView(int PlanId);

        bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan);
        bool TogglePlanActivation(int PlanId);
    }
}
