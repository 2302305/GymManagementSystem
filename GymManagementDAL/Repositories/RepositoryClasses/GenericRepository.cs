

using System.Linq.Expressions;

namespace GymManagementDAL.Repositories.RepositoryClasses
{
    public class GenericRepository<TEntity>(GymDbContext gymDbContext)
        : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {


        public TEntity? GetById(int Id)
        {
            var entity = gymDbContext.Set<TEntity>().Find(Id);
            if (entity == null)
                return null;
            return entity;

        }
        public void Add(TEntity entity) => gymDbContext.Set<TEntity>().Add(entity);


        public void Update(TEntity entity) => gymDbContext.Set<TEntity>().Update(entity);



        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition is not null)
            {
                return gymDbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();
            }
            return [.. gymDbContext.Set<TEntity>().AsNoTracking()];
        }

        public void Delete(TEntity entity) => gymDbContext.Set<TEntity>().Remove(entity);

        public IEnumerable<TEntity> GetAllWithIncludes(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = gymDbContext.Set<TEntity>().AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return query.ToList();
        }
    }
}
