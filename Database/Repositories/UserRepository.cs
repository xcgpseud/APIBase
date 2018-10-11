using System.Linq;
using Database.Context;
using Database.Entities;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MainContext _context;

        public UserRepository(MainContext context)
        {
            _context = context;
        }
        
        public UserEntity Create(UserEntity entity)
        {
            var result = _context.Users.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public UserEntity Get(long id)
        {
            return _context.Users.FirstOrDefault(user => user.UserId == id);
        }

        public UserEntity GetBy<T>(string property, T value)
        {
            return _context.Users.FirstOrDefault(user =>
                EF.Property<T>(user, property).Equals(value));
        }

        public UserEntity Update(UserEntity newEntity)
        {
            var entityToUpdate = Get(newEntity.UserId);
            if (entityToUpdate == null) return null;
            _context.Entry(entityToUpdate).CurrentValues.SetValues(newEntity);
            _context.SaveChanges();
            return newEntity;
        }

        public void Delete(long id)
        {
            var entityToDelete = Get(id);
            if (entityToDelete == null) return;
            _context.Users.Remove(entityToDelete);
            _context.SaveChanges();
        }
    }
}