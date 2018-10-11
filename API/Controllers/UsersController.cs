using Database.Entities;
using Domain.Models;
using Logic.Helpers;
using Logic.Logics.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIBase.Controllers
{
    // TODO: Sort out proper error responses throughout
    [Route("api/users")]
    public class UsersController
    {
        private readonly IUserLogic _logic;

        public UsersController(IUserLogic logic)
        {
            _logic = logic;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            var entity = Mapper.Map<User, UserEntity>(user);
            var result = _logic.Create(entity);
            return new OkObjectResult(Mapper.Map<UserEntity, User>(result));
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public IActionResult GetUser(long userId)
        {
            var result = _logic.Get(userId);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(Mapper.Map<UserEntity, User>(result));
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            var entity = Mapper.Map<User, UserEntity>(user);
            var result = _logic.Update(entity);
            return new OkObjectResult(Mapper.Map<UserEntity, User>(result));
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(long userId)
        {
            _logic.Delete(userId);
            return new NoContentResult();
        }
    }
}