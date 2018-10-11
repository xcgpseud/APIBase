using Database.Entities.Interfaces;

namespace Logic.Logics.Interfaces
{
    public interface ILogic<TEntity> where TEntity : IEntity, new()
    {
        TEntity Create(TEntity entity);

        TEntity Get(long id);

        TEntity Update(TEntity newEntity);

        void Delete(long id);
    }
}