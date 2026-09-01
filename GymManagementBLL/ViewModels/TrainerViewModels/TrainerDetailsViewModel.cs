namespace GymManagementBLL.ViewModels.TrainerViewModel
{
    public class TrainerDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public Specialities Specialization { get; set; }
        //Get Trainer Details
        public int BuildingNumber { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
    }
}
