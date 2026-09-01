using GymManagementBLL.ViewModels.MemberViewModel;

namespace GymManagementBLL.AutoMapping
{
    public class HealthRecordMappingProfile : Profile
    {
        public HealthRecordMappingProfile()
        {

            CreateMap<HealthRecord, HealthRecordViewModel>().ReverseMap();

        }
    }
}
