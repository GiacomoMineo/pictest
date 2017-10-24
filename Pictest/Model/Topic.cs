using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Pictest.Persistence.Storage;

namespace Pictest.Model
{
    public class Topic
    {
        [Required]
        public string Name { get; set; }
        public List<string> Tags { get; set; }
    }
}
