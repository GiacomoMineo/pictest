using System.ComponentModel.DataAnnotations;

namespace Pictest.Service.Request
{
    public class CreateContestRequest
    {
        [Required]
        public string Topic { get; set; }
        public bool Current { get; set; }
    }
}