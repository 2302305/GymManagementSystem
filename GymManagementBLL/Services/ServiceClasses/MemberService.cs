using GymManagementBLL.Services.Attachments;
using GymManagementBLL.ViewModels.MemberViewModel;
namespace GymManagementBLL.Services.RepositoryClasses
{
    public class MemberService(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentService attachmentService) : IMemberServices
    {
        public bool CreateMember(CreateMemberviewModel createdMember)
        {
            try
            {
                if (EmailExsistence(createdMember.Email) || PhoneExsistence(createdMember.Phone)) return false;
                //Attachment 
                var PhotoName = attachmentService.Upload("Members", createdMember.PhotoFile);
                if (string.IsNullOrEmpty(PhotoName)) return false;

                var Member = mapper.Map<CreateMemberviewModel, Member>(createdMember);
                Member.Photo = PhotoName;
                //if there is an error in the savechanging process the photo cancels the upload 
                unitOfWork.GetRepository<Member>().Add(Member);//Locally
                var Result = unitOfWork.SaveChanges() > 0;
                if (!Result)
                {
                    attachmentService.Delete(PhotoName, "Members");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = unitOfWork.GetRepository<Member>().GetAll() ?? [];
            if (Members == null || !Members.Any()) return [];
            var MemberViewModels = mapper.Map<IEnumerable<MemberViewModel>>(Members);
            return MemberViewModels;
        }
        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var MemberDetails = unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (MemberDetails is null) return null;


            //new MemberViewModel()
            //{
            //    Name = MemberDetails.Name,
            //    Email = MemberDetails.Email,
            //    Phone = MemberDetails.Phone,
            //    Gender = MemberDetails.Gender.ToString(),
            //    Address = $"
            //    {MemberDetails.Address.BuildingNumber} -
            //    {MemberDetails.Address.Street} -
            //    {MemberDetails.Address.City}
            //    ",
            //    DateOfBirth = MemberDetails.DateOfBirth.ToShortDateString(),
            //    Photo = MemberDetails.Photo,
            //    ///way 01
            //    //PlanName = MemberDetails.Memberships.Select(s => s.Plan.Name).ToString(),
            //    //StartDate = MemberDetails.CreatedAt.ToShortDateString(),
            //    //EndDate = MemberDetails.Memberships.Select(s => s.EndDate).ToString(),

            //};
            //way02
            var MemberViewModels = mapper.Map<MemberViewModel>(MemberDetails);
            var ActiveMembership = unitOfWork.GetRepository<Membership>()
                .GetAll(m => m.MemberId == MemberId)
                .Where(w => w.Status == "Active")
                .FirstOrDefault();
            if (ActiveMembership is not null)
            {
                var PlanName = unitOfWork.GetRepository<Plan>()
                  .GetById(ActiveMembership.PlanId);

                MemberViewModels.StartDate = ActiveMembership.CreatedAt.ToShortDateString();
                MemberViewModels.EndDate = ActiveMembership.EndDate.ToShortDateString();
                MemberViewModels.PlanName = PlanName?.Name;
            }

            return MemberViewModels;

        }

        public HealthRecordViewModel? GetHealthRecordDetails(int MemberId)
        {
            var HealthRecordRepo = unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
            if (HealthRecordRepo == null) return null;
            return mapper.Map<HealthRecordViewModel>(HealthRecordRepo);
        }

        public UpdateMemberViewModel? MemberUpdateViewModel(int MemberId)
        {
            var Member = unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member is null) return null;
            return mapper.Map<UpdateMemberViewModel>(Member);
        }

        public bool MemberUpdate(int MemberId, UpdateMemberViewModel updatedMember)
        {
            try
            {
                var EmailExists = unitOfWork.GetRepository<Member>()
                    .GetAll(x => x.Email == updatedMember.Email && x.Id != MemberId);
                var PhoneExists = unitOfWork.GetRepository<Member>()
                    .GetAll(x => x.Phone == updatedMember.Phone && x.Id != MemberId);
                if (EmailExists.Any() || PhoneExists.Any()) return false;

                var Member = unitOfWork.GetRepository<Member>().GetById(MemberId);
                if (Member is null) return false;

                mapper.Map(updatedMember, Member);
                unitOfWork.GetRepository<Member>().Update(Member);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //HelperMethod Made For checking the existence of Email and Phone 
        #region HelperMethod 
        private bool EmailExsistence(string Email)
        {
            return unitOfWork.GetRepository<Member>().GetAll(g => g.Email == Email).Any();

        }
        private bool PhoneExsistence(string Phone)
        {
            return unitOfWork.GetRepository<Member>().GetAll(g => g.Phone == Phone).Any();

        }
        #endregion
        public bool DeleteMember(int MemberId)
        {
            var Member = unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member is null) return false;
            //Cannot Delete Members with Active Bookings 
            var SessionIds = unitOfWork.GetRepository<MemberSessionRL>()
                .GetAll(g => g.MemberId == MemberId).Select(X => X.SessionId);

            var HasFutureSessions = unitOfWork.GetRepository<Session>().GetAll(X =>
            SessionIds.Contains(X.Id) && X.StartDate > DateTime.Now).Any();

            if (HasFutureSessions) return false;

            var memberShips = unitOfWork.GetRepository<Membership>().GetAll(g => g.MemberId == MemberId);
            try
            {
                if (memberShips.Any())
                {
                    foreach (var membership in memberShips)
                    {
                        unitOfWork.GetRepository<Membership>().Delete(membership);
                    }
                }
                unitOfWork.GetRepository<Member>().Delete(Member);
                if (unitOfWork.SaveChanges() > 0)
                {
                    attachmentService.Delete(Member.Photo, "Members");
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}