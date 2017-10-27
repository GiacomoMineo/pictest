using System.ComponentModel.DataAnnotations;

namespace Pictest.Service.Request
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
