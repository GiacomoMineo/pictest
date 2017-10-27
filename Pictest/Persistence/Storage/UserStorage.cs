using MongoDB.Bson.Serialization.Attributes;

namespace Pictest.Persistence.Storage
{
    [BsonIgnoreExtraElements]
    public class UserStorage
    {
        [BsonId]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}