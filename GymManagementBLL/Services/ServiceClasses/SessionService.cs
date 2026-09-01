
namespace GymManagementBLL.Services.ServiceClasses
{
    public class SessionService(IUnitOfWork unitOfWork, IMapper mapper) : ISessionServices
    {
        public SessionDetailsViewModel? GetSessionDetails(int sessionId)
        {
            var session = unitOfWork.SessionRepository
                                    .GetSessionsWithTrainerAndCategoryById(sessionId);

            if (session is null) return null;

            // ✅ Manual map — avoids the InvalidCastException from AutoMapper
            var mapped = new SessionDetailsViewModel
            {
                Id = session.Id,
                CategoryName = session.SessionCategory.CategoryName,
                TrainerName = session.TrainerSession.Name,
                Description = session.Description,
                Capacity = session.Capacity,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                AvailableSlots = session.Capacity -
                                 unitOfWork.SessionRepository
                                           .GetCountOfBookedSlots(sessionId)
            };

            // ✅ Load assigned members with attendance
            var bookings = unitOfWork.GetRepository<MemberSessionRL>()
                                     .GetAllWithIncludes(b => b.Member)
                                     .Where(b => b.SessionId == sessionId);

            mapped.AssignedMembers = bookings.Select(b => new MemberSessionViewModel
            {
                MemberId = b.MemberId,
                MemberName = b.Member.Name,
                BookedAt = b.CreatedAt,
                IsAttended = b.IsAttended
            }).ToList();

            return mapped;
        }
        public bool CreateSession(CreateSessionViewModel CreatedSession)
        {

            try
            {
                if (!IsTrainerExists(CreatedSession.TrainerId)) return false;

                if (!IsCategoryExists(CreatedSession.CategoryId)) return false;

                if (!DateCheck(CreatedSession.StartDate, CreatedSession.EndDate)) return false;

                if (CreatedSession.Capacity <= 0 || CreatedSession.Capacity > 25) return false;
                var Session = mapper.Map<CreateSessionViewModel, Session>(CreatedSession);
                unitOfWork.SessionRepository.Add(Session);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Sessions = unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (Sessions == null) return [];
            //Manual Mapping
            //return Sessions.Select(s => new SessionViewModel()
            //{
            //    Id = s.Id,
            //    Description = s.Description,
            //    StartDate = s.StartDate,
            //    EndDate = s.EndDate,
            //    Capacity = s.Capacity,
            //    TrainerName = s.TrainerSession.Name,//Related Data //Not Loaded Data
            //    CategoryName = s.SessionCategory.CategoryName,//Related Data //Not Loaded Data
            //    //Available Slots ----> Computed Data [Capacity - Count of Booking Sessions]
            //    AvailableSlots = s.Capacity - unitOfWork.SessionRepository.GetCountOfBookedSlots(s.Id)
            //});
            //Auto Mapper
            var MappedSessions = mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(Sessions);
            foreach (var session in MappedSessions)
            {
                session.AvailableSlots = session.Capacity - unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);
            }
            return MappedSessions;

        }

        public SessionViewModel? GetSessionById(int sessionId)
        {
            var Session = unitOfWork.SessionRepository.GetSessionsWithTrainerAndCategoryById(sessionId);
            if (Session == null) return null;
            //return new SessionViewModel
            //{
            //    //AvailableSlots = Session.Capacity - SessionRepo.GetCountOfBookedSlots(Session.Id),
            //    Capacity = Session.Capacity,
            //    CategoryName = Session.SessionCategory.CategoryName,
            //    TrainerName = Session.TrainerSession.Name,
            //    Description = Session.Description,
            //    StartDate = Session.StartDate,
            //    EndDate = Session.EndDate
            //};
            var MappedSession = mapper.Map<Session, SessionViewModel>(Session);
            MappedSession.AvailableSlots = MappedSession.Capacity - unitOfWork.SessionRepository.GetCountOfBookedSlots(MappedSession.Id);
            return MappedSession;

        }
        public UpdateSessionViewModel? UpdateSessionViewModel(int SessionId)
        {
            var Session = unitOfWork.SessionRepository.GetById(SessionId);
            if (!IsTrainerExists(Session!.TrainerId)) return null;

            if (!DateCheck(Session.StartDate, Session.EndDate)) return null;

            if (Session.Capacity <= 0 || Session.Capacity > 25) return null;

            if (!IsSessionAvailableForUpdate(Session)) return null;

            var SessionMapping = mapper.Map<Session, UpdateSessionViewModel>(Session);
            return SessionMapping;
        }

        public bool UpdateSession(UpdateSessionViewModel UpdatedSession, int SessionId)
        {
            try
            {
                var Session = unitOfWork.SessionRepository.GetById(SessionId);

                // Check current session state is eligible for update
                if (!IsSessionAvailableForUpdate(Session!)) return false;

                // Validate the NEW submitted data
                if (!IsTrainerExists(UpdatedSession.TrainerId)) return false;
                if (!DateCheck(UpdatedSession.StartDate, UpdatedSession.EndDate)) return false;

                // Apply and save
                mapper.Map(UpdatedSession, Session);
                Session.UpdatedAt = DateTime.Now;
                unitOfWork.SessionRepository.Update(Session);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool DeleteSession(int SessionId)
        {
            try
            {
                var session = unitOfWork.SessionRepository.GetById(SessionId);
                if (session == null) return false;

                // Can't delete Upcoming or Ongoing sessions
                if (session.StartDate > DateTime.Now) return false;   // Upcoming
                if (session.EndDate >= DateTime.Now) return false;    // Ongoing

                // Only Past sessions reach this point
                unitOfWork.SessionRepository.Delete(session);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #region HelperMethods -- Trainer-Category-Check -- Date Check
        private bool IsSessionAvailableForUpdate(Session session)
        {
            var SessionAvailableSlots = unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);
            if (session == null) return false;
            //if session ongoing or completed u cant update it and if it has active bookings
            //completed
            if (session.EndDate < DateTime.Now) return false;
            //Ongoing
            if (session.StartDate <= DateTime.Now) return false;
            //GetCount of Slots
            if (SessionAvailableSlots > 0) return false;
            return true;

        }
        private bool IsTrainerExists(int TrainerId)
        {
            return unitOfWork.GetRepository<Trainer>().GetById(TrainerId) is not null;
        }
        private bool IsCategoryExists(int CategoryId)
        {
            return unitOfWork.GetRepository<Category>().GetById(CategoryId) is not null;
        }
        private static bool DateCheck(DateTime StartDate, DateTime EndDate)
        {
            return StartDate < EndDate && DateTime.Now < StartDate;
        }
        #endregion
        public IEnumerable<TrainerSelectViewModel> GetTrainersForDropDown()
        {
            var Trainer = unitOfWork.GetRepository<Trainer>().GetAll();

            return Trainer.Select(s => new TrainerSelectViewModel()
            {
                TrainerId = s.Id,
                TrainerName = s.Name
            });
        }

        public IEnumerable<CategorySelectViewModel> GetCategoriesForDropDown()
        {
            var Categories = unitOfWork.GetRepository<Category>().GetAll();
            return Categories.Select(c => new CategorySelectViewModel()
            {
                CategoryId = c.Id,
                CategoryName = c.CategoryName
            });
        }

    }
}
