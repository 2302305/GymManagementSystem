namespace GymManagementDAL.Entities
{
    abstract public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        //I can update if i want thats why its nullable
        public DateTime? UpdatedAt { get; set; }
    }
}
