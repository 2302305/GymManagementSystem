namespace GymManagementDAL.Entities
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }

        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }

        #region Sesion-Category Relation

        public int CategoryId { get; set; }
        public Category SessionCategory { get; set; } = null!;

        #endregion

        #region Session-Trainer Relation
        public Trainer TrainerSession { get; set; } = null!;
        public int TrainerId { get; set; }
        #endregion
        //Each Session could be booked by multiple Members 
        public ICollection<MemberSessionRL> MemberSessions { get; set; } = null!;

    }
}
