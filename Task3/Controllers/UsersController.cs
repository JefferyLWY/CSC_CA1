using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task3.Models;
using Task3.UserData;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task3.Controllers
{
    [ApiController, Route("api/users"), Authorize]
    public class UsersController : ControllerBase
    {
        private IUserRepository userRepository = new UserRepository();

        #region User Controllers
        //1. Get Users
        [HttpGet, Route("{inputId:int:min(1)}")]
        public User GetUser(int inputId) => userRepository.Get(inputId);

        //2. Get User by Id
        [HttpGet]
        public IEnumerable<User> GetAllUsers() => userRepository.GetAll();

        //3. Create User
        [HttpPost, Authorize(Policy = "AdminOnly")]
        public IActionResult PostUser(User inputUser)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest("Invalid object as parameter."); }

                User tempUser = userRepository.Get(inputUser.Id);
                if (tempUser != null) { return Conflict("User with the id of " + inputUser.Id + " already exists."); }

                userRepository.Post(inputUser);
                return Ok("User with the id of " + inputUser.Id + " was successfully created.");
            }
            catch (Exception) { return StatusCode(500); }
        }

        //4. Update User
        [HttpPut, Route("{inputId:int:min(1)}"), Authorize(Policy = "AdminOnly")]
        public IActionResult PutUser(int inputId, User inputUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid object as parameter.");
                }

                User tempUser = userRepository.Get(inputId);
                if (tempUser == null) { return NotFound("User with the id of " + inputUser.Id + " does not exist."); }
                if (tempUser.Id != inputUser.Id)
                {
                    tempUser = userRepository.Get(inputUser.Id);
                    if (tempUser != null) { return Conflict("Potential conflict of proposed Id detected."); }
                }

                userRepository.Put(inputId, inputUser);
                return Ok("User with the id of " + inputUser.Id + " was successfully updated.");
            }
            catch (Exception) { return StatusCode(500); }
        }

        //4. Delete User
        [HttpDelete, Route("{inputId:int:min(1)}"), Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteUser(int inputId)
        {
            try
            {
                User tempUser = userRepository.Get(inputId);
                if (tempUser == null)
                {
                    return NotFound("User with the id of " + inputId + " does not exist.");
                }

                userRepository.Delete(inputId);
                return Ok("User with the id of " + inputId + " was successfully deleted.");

            }
            catch (Exception) { return StatusCode(500); }
        }
        #endregion
    }
}
