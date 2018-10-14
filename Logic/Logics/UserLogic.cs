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
        
        public Response<User> Create(User user)
        {
            var entity = Mapper.Map<User, UserEntity>(user);
            var result = _repository.Create(entity);
            return new Response<User>
            {
                ResponseObject = Mapper.Map<UserEntity, User>(result)
            };
        }

        public Response<User> Get(long id)
        {
            return new Response<User>
            {
                ResponseObject = Mapper.Map<UserEntity, User>(_repository.Get(id))
            };
        }

        public Response<User> Update(User user)
        {
            var entity = Mapper.Map<User, UserEntity>(user);
            var result = _repository.Update(entity);
            return new Response<User>
            {
                ResponseObject = Mapper.Map<UserEntity, User>(result)
            };
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}