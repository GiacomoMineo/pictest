using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pictest.Service.Request
{
    public class CreateContestRequest
    {
        [Required]
        public string Topic { get; set; }
        public bool Current { get; set; }
    }
}