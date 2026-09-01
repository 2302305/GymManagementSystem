using GymManagementDAL.Entities.Address;
using GymManagementDAL.Entities.Enums;

namespace GymManagementDAL.Entities
{
    public class GymUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public CompositeAddressProperty Address { get; set; } = null!;
    }
}
