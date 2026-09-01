using GymManagementDAL.Entities.Enums;

namespace GymManagementDAL.Entities
{
    public class Trainer : GymUser
    {
        //Hiring Date = Created At ->FluentApi
        public Specialities Specialities { get; set; }
        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
