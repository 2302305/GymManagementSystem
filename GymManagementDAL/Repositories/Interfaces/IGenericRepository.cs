using System.Linq.Expressions;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null);
        TEntity? GetById(int Id);
        void Update(TEntity entity);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAllWithIncludes(params Expression<Func<TEntity, object>>[] includes);

    }
}
