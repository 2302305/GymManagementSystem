namespace GymManagementBLL.AutoMapping
{
    public class SessionMappingProfile : Profile
    {
        public SessionMappingProfile()
        {           //Source      //Destination
            CreateMap<Session, SessionViewModel>()
               .ForMember(dest => dest.CategoryName, Options =>
               Options.MapFrom(src => src.SessionCategory.CategoryName))
               .ForMember(dest => dest.TrainerName, Options =>
               Options.MapFrom(src => src.TrainerSession.Name))
               .ForMember(Dest => Dest.AvailableSlots, Options => Options.Ignore());//Resolver Wait.....


            //NoConfigurationNeeded

            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<UpdateSessionViewModel, Session>().ReverseMap();
        }
    }
}
