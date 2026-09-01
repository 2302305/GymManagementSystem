using GymManagementBLL.ViewModels.MemberViewModel;

namespace GymManagementBLL.AutoMapping
{
    public class MemberMappingProfile : Profile
    {
        public MemberMappingProfile()
        {
            //CreateMap<CreateMemberviewModel, Member>().ForMember(dest => dest.Address, Option =>
            //{
            //    Option.MapFrom(src => new CompositeAddressProperty
            //    {
            //        BuildingNumber = src.BuildingNumber,
            //        Street = src.Street,
            //        City = src.City
            //    });
            //});
            CreateMap<CreateMemberviewModel, Member>()
                .ForMember(dest => dest.Address, Options => Options.MapFrom(src => src))
                .ForMember(dest => dest.HealthRecord, Options => Options.MapFrom(src => src.HealthRecordViewModel));


            CreateMap<CreateMemberviewModel, CompositeAddressProperty>()
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));

            CreateMap<HealthRecord, HealthRecordViewModel>().ReverseMap();

            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.Gender,
                Options => Options.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.DateOfBirth,
                 Options => Options.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                .ForMember(dest => dest.Address,
                 Options => Options.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street}-{src.Address.City}"));
            CreateMap<Member, UpdateMemberViewModel>()
                .ForMember(dest => dest.Street, Option => Option.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, Option => Option.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.BuildingNumber, Option => Option.MapFrom(src => src.Address.BuildingNumber))
            ;
            CreateMap<UpdateMemberViewModel, Member>()
                .ForMember(dest => dest.Name, Opt => Opt.Ignore())
                .ForMember(dest => dest.Photo, Opt => Opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Street = src.Street;
                    dest.Address.City = src.City;
                    dest.UpdatedAt=DateTime.Now;
                });

        }
    }
}
