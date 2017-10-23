using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pictest.Persistence.Interface;

namespace Pictest.Persistence.Repository
{
    public class MongoDbRepository : IMongoDbRepository
    {
        private readonly MongoClient _mongoClient;

        public MongoDbRepository(IOptions<MongoDbOptions> options)
        {
            _mongoClient = new MongoClient(options.Value.Url);
        }

        public IMongoDatabase GetDatabase(string dbName)
        {
            return _mongoClient.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T>(string dbName, string collectionName)
        {
            return GetDatabase(dbName).GetCollection<T>(collectionName);
        }
    }
}
