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
            var result = _logic.Create(user);
            return new OkObjectResult(result);
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public IActionResult GetUser(long userId)
        {
            var result = _logic.Get(userId);
            if (result.ResponseObject == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            var result = _logic.Update(user);
            return new OkObjectResult(result);
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(long userId)
        {
            _logic.Delete(userId);
            return new NoContentResult();
        }
    }
}