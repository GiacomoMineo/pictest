using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pictest.Persistence.Storage
{
    [BsonIgnoreExtraElements]
    public class UserStorage
    {
        [BsonId]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}