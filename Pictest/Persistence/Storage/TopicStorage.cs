﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pictest.Persistence.Storage
{
    [BsonIgnoreExtraElements]
    public class TopicStorage
    {
        [BsonId]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Tags { get; set; }
    }
}