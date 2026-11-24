using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; } = "";
        
        [Required]
        public string LastName { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string Department { get; set; } = "";

        [Required]
        public string Position { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}