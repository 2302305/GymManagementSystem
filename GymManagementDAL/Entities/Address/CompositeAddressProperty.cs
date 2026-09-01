using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Entities.Address
{
    [Owned]
    public class CompositeAddressProperty
    {
        public int BuildingNumber { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
