
namespace GymManagementBLL.AutoMapping
{
    public class TrainerMappingProfile : Profile
    {
        public TrainerMappingProfile()
        {
            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.Address.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Address.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber));




            CreateMap<Trainer, TrainersViewModel>();
            CreateMap<Trainer, TrainerUpdateViewModel>()
                .ForMember(dist => dist.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dist => dist.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dist => dist.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber));

            CreateMap<TrainerUpdateViewModel, Trainer>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.BuildingNumber = src.BuildingNumber;
                dest.Address.City = src.City;
                dest.Address.Street = src.Street;
                dest.UpdatedAt = DateTime.Now;
            });

        }
    }
}
