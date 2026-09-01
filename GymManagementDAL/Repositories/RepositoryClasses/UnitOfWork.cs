namespace GymManagementDAL.Repositories.RepositoryClasses
{
    public class UnitOfWork(GymDbContext gymDbContext, ISessionRepository sessionRepository) : IUnitOfWork
    {
        private readonly Dictionary<Type, object> repositories = [];
        public ISessionRepository SessionRepository { get; } = sessionRepository;
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var Entity = typeof(TEntity);
            if (repositories.TryGetValue(Entity, out var Repo))
                return (IGenericRepository<TEntity>)Repo;
            var NewRepo = new GenericRepository<TEntity>(gymDbContext);
            repositories[Entity] = new GenericRepository<TEntity>(gymDbContext);
            return NewRepo;
        }

        public int SaveChanges()
        {
            return gymDbContext.SaveChanges();
        }
    }
}
