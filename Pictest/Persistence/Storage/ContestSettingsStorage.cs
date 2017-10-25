using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pictest.Persistence.Storage
{
    [BsonIgnoreExtraElements]
    public class ContestSettingsStorage
    {
        [BsonId]
        public string Id { get; set; }

        public string CurrentId { get; set; }
    }
}