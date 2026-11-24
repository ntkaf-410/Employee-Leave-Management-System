using System.Runtime.CompilerServices;

namespace API.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}