using Database.Entities.Interfaces;

namespace Database
{
    public class RepositoryResponse<TEntity> where TEntity : IEntity, new()
    {
        private RepositoryResponse()
        {}
        
        public bool Success { get; private set; }
        public TEntity Entity { get; private set; }
        
        public static RepositoryResponseBuilder<TEntity> Create()
        {
            return new RepositoryResponseBuilder<TEntity>();
        }
        
        public class RepositoryResponseBuilder<TEntity> where TEntity : IEntity, new()
        {
            private bool _success = false;
            private TEntity _entity;
            
            public RepositoryResponseBuilder<TEntity> WithSuccess(bool success)
            {
                _success = success;
                return this;
            }

            public RepositoryResponseBuilder<TEntity> WithEntity(TEntity entity)
            {
                _entity = entity;
                return this;
            }

            public RepositoryResponse<TEntity> Build()
            {
                if (_success && _entity != null) // Can this be here to ensure null isn't allowed to be passed around?
                {
                    return new RepositoryResponse<TEntity>
                    {
                        Success = _success,
                        Entity = _entity
                    };
                }

                return new RepositoryResponse<TEntity>
                {
                    Success = false
                };
            }
        }
    }
}