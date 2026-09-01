namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        //GetAll Sessions
        IEnumerable<Session> GetAllSessionsWithTrainerAndCategory();
        int GetCountOfBookedSlots(int sessionnId);
        Session? GetSessionsWithTrainerAndCategoryById(int sessionnId);
    }
}
