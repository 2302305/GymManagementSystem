using GymManagementDAL.Entities.Enums;

namespace GymManagementDAL.Entities;
public class HealthRecord : BaseEntity
{

    public decimal Hieght { get; set; }
    public decimal Weight { get; set; }
    public BloodType BloodType { get; set; }
    public string? Note { get; set; }
}
