using Database.Entities.Interfaces;

namespace Database.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity, new()
    {
        TEntity Create(TEntity entity);

        TEntity Get(long id);

        TEntity GetBy<T>(string property, T value);

        TEntity Update(TEntity newEntity);

        void Delete(long id);
    }
}