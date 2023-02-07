using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _usersService.GetUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _usersService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _usersService.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            var existingUser = _usersService.GetUser(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            _usersService.UpdateUser(user);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = _usersService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            _usersService.DeleteUser(user);
            return NoContent();
        }
    }
}
