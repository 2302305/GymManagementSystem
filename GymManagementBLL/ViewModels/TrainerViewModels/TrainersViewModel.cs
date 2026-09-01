namespace GymManagementBLL.ViewModels.TrainerViewModel
{
    public class TrainersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public Specialities Specialities { get; set; }

    }
}
