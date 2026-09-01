namespace GymManagementBLL.Services.Interfaces
{
    public interface ITrainerServices
    {
        IEnumerable<TrainersViewModel> GetAllTrainers();

        bool CreateTrainer(CreateTrainerViewModel createTrainerViewModel);

        TrainerDetailsViewModel? GetTrainerById(int Trainerid);

        TrainerUpdateViewModel? UpdateTrainerViewModel(int Trainerid);
        bool UpdateTrainer(int Trainerid, TrainerUpdateViewModel trainerUpdateViewModel);

        bool DeleteTrainer(int Trainerid);

    }
}
