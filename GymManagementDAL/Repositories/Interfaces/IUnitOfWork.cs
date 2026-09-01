namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public ISessionRepository SessionRepository { get; }
        int SaveChanges();
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();
    }
}
