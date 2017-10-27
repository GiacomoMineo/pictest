using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pictest.Persistence.Storage
{
    [BsonIgnoreExtraElements]
    public class ContestStorage
    {
        [BsonId]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Topic { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Voters { get; set; }
        public bool? Closed { get; set; }
        public string Winner { get; set; }
    }
}