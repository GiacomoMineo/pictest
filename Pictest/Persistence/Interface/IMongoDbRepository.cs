using MongoDB.Driver;

namespace Pictest.Persistence.Interface
{
    public interface IMongoDbRepository
    {
        IMongoDatabase GetDatabase(string dbName);
        IMongoCollection<T> GetCollection<T>(string dbName, string collectionName);
    }
}
