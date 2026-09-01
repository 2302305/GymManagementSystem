namespace GymManagementBLL.Services.Interfaces
{
    public interface ISessionServices
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int SessionId);
        SessionDetailsViewModel? GetSessionDetails(int SessionId); // ✅ new
        bool CreateSession(CreateSessionViewModel CreatedSession);
        UpdateSessionViewModel? UpdateSessionViewModel(int SessionId);
        bool UpdateSession(UpdateSessionViewModel UpdatedSession, int SessionId);
        bool DeleteSession(int SessionId);
        IEnumerable<TrainerSelectViewModel> GetTrainersForDropDown();
        IEnumerable<CategorySelectViewModel> GetCategoriesForDropDown();
    }
}