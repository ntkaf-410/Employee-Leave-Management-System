using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // localhost:5294/api/auth
    public class EmployeeController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetEmployees()
        {
            var employees = await context.Users.ToListAsync();
            return employees;
        }

        [HttpGet]
        public async Task<ActionResult<AppUser>> GetEmployee( string id )
        {
            var employee = await context.Users.FindAsync(id);
            if (employee == null) return NotFound();
            return employee;     
        }
        

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            if (await EmalExists(registerDto.Email)) return BadRequest("Email taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Department = registerDto.Department,
                Position = registerDto.Position,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        private async Task<Boolean> EmalExists(string Email)
        {
            return await context.Users.AnyAsync(x => x.Email.ToLower() == Email.ToLower());
        }
    }
}