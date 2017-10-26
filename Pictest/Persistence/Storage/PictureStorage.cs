using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Pictest.Persistence.Storage
{
    public class PictureStorage
    {
        [BsonId]
        public string Id { get; set; }
        public string Url { get; set; }
        public string Caption { get; set; }
        public string UserId { get; set; }
        public string ContestId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
