using GrievanceAPI.Data;
using GrievanceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GrievanceAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // REGISTER USER
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                message = "User registered successfully"
            });
        }
        [HttpPost("login")]
        public IActionResult Login(User loginUser)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Email == loginUser.Email &&
                u.Password == loginUser.Password);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password"
                });
            }

            return Ok(new
            {
                message = "Login successful",
                user.Id,
                user.Name,
                user.Email,
                user.Role
            });
        }
    }
}