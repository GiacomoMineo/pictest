using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pictest.Persistence.Storage
{
    [BsonIgnoreExtraElements]
    public class PictureStorage
    {
        [BsonId]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Url { get; set; }
        public string Caption { get; set; }
        public string UserId { get; set; }
        public string ContestId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? Votes { get; set; }
    }
}
