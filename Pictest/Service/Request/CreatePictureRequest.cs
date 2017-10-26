using System.ComponentModel.DataAnnotations;

namespace Pictest.Service.Request
{
    public class CreatePictureRequest
    {
        [Required]
        public string ContestId { get; set; }
        public string Caption { get; set; }
    }
}