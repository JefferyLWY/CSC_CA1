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

        #region UsersController - Crud Methods
        //Create Method
        [HttpPost, Authorize(Policy = "AdminOnly")]
        public IActionResult PostUser(User inputUser)
        {
            try
            {
                #region PostUser - Model Validation
                if (!ModelState.IsValid) { return BadRequest("Invalid object as parameter."); }
                #endregion
                #region PostUser - Conflict Validation
                User tempUser = userRepository.Get(inputUser.Id);
                if (tempUser != null) { return Conflict("User with the id of " + inputUser.Id + " already exists."); }
                #endregion
                #region PostUser - Data Processing
                userRepository.Post(inputUser);
                return Ok("User with the id of " + inputUser.Id + " was successfully created.");
                #endregion
            }
            catch (Exception) { return StatusCode(500); }
        }

        //Read Methods
        [HttpGet, Route("{inputId:int:min(1)}")]
        public User GetUser(int inputId) => userRepository.Get(inputId);
        [HttpGet]
        public IEnumerable<User> GetAllUsers() => userRepository.GetAll();

        //Update Method
        [HttpPut, Route("{inputId:int:min(1)}"), Authorize(Policy = "AdminOnly")]
        public IActionResult PutUser(int inputId, User inputUser)
        {
            try
            {
                #region PutUser - Model Validation
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid object as parameter.");
                }
                #endregion
                #region PutUser - Content Validation
                User tempUser = userRepository.Get(inputId);
                if (tempUser == null) { return NotFound("User with the id of " + inputUser.Id + " does not exist."); }
                if (tempUser.Id != inputUser.Id)
                {
                    tempUser = userRepository.Get(inputUser.Id);
                    if (tempUser != null) { return Conflict("Potential conflict of proposed Id detected."); }
                }
                #endregion
                #region PutUser - Data Processing
                userRepository.Put(inputId, inputUser);
                return Ok("User with the id of " + inputUser.Id + " was successfully updated.");
                #endregion
            }
            catch (Exception) { return StatusCode(500); }
        }

        //Delete Method
        [HttpDelete, Route("{inputId:int:min(1)}"), Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteUser(int inputId)
        {
            try
            {
                #region DeleteUser - Content Validation
                User tempUser = userRepository.Get(inputId);
                if (tempUser == null)
                {
                    return NotFound("User with the id of " + inputId + " does not exist.");
                }
                #endregion
                #region DeleteUser - Data Processing
                userRepository.Delete(inputId);
                return Ok("User with the id of " + inputId + " was successfully deleted.");
                #endregion
            }
            catch (Exception) { return StatusCode(500); }
        }
        #endregion
    }
}
