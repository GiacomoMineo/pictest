using System;
using System.Collections.Generic;

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

        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Tags { get; set; }
    }
}
