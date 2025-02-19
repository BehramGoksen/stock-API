using Microsoft.AspNetCore.Mvc;
using Entity;
using Services.Abstract;

namespace StokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IService<User> _service;
        public UserController(IService<User> service)
        {
            _service = service;
        }

        [HttpGet("UserList")]
        public IActionResult GetAllUsers()
        {
            var allUsers = _service.GetAll();
            return Ok(allUsers);
        }

        [HttpGet("FindUser")]
        public IActionResult GetUser(int id)
        {
            var user = _service.Find(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found with the provided ID." });
            }
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser(User user)
        {

            _service.Add(user);
            _service.Save();
            return Ok("User has been created succesfully");

        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {

            var user = _service.Find(id);

            if (user == null)
            {
                return NotFound("User not found");
            }
            _service.Delete(user);
            _service.Save();
            return Ok("User has been deleted successfully");
        }


        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            var existingUser = _service.Find(id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }
            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Rol = user.Rol;

            _service.Save();

            return Ok("user has been updated successfully");
        }
        

    }
}
