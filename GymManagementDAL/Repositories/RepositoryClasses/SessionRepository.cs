namespace GymManagementDAL.Repositories.RepositoryClasses
{
    public class SessionRepository(GymDbContext gymDbContext) : GenericRepository<Session>(gymDbContext), ISessionRepository
    {
        private readonly GymDbContext gymDbContext = gymDbContext;
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
            => [..
                gymDbContext.Sessions
                .Include(I => I.TrainerSession)
                .Include(I => I.SessionCategory)
               ];

        public int GetCountOfBookedSlots(int sessionnId) =>
             //Capacity - Booked Sessions
             gymDbContext.MemberSessionRLs.Count(C => C.SessionId == sessionnId);

        public Session? GetSessionsWithTrainerAndCategoryById(int sessionnId)
            => gymDbContext.Sessions
                 .Include(I => I.TrainerSession)
                 .Include(I => I.SessionCategory)
                 .FirstOrDefault(f => f.Id == sessionnId);

    }
}
