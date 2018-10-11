using Database.Entities;
using Database.Repositories.Interfaces;
using Domain.Models;
using Logic.Helpers;
using Logic.Logics.Interfaces;
using SQLitePCL;

namespace Logic.Logics
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _repository;

        public UserLogic(IUserRepository repository)
        {
            _repository = repository;
        }
        
        public UserEntity Create(UserEntity entity)
        {
            return _repository.Create(entity);
        }

        public UserEntity Get(long id)
        {
            return _repository.Get(id);
        }

        public UserEntity Update(UserEntity newEntity)
        {
            return _repository.Update(newEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}