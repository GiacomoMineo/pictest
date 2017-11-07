using System.ComponentModel.DataAnnotations;

namespace Pictest.Service.Request
{
    public class CreateUserRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
