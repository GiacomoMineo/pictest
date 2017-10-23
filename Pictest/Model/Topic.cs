using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pictest.Model
{
    public class Topic
    {
        public Topic(string name, DateTime createdAt, List<string> tags)
        {
            Name = name;
            CreatedAt = createdAt;
            Tags = tags;
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public List<string> Tags { get; set; }
    }
}
