using System.Threading.Tasks;
using MongoDB.Driver;
using Pictest.Persistence.Interface;
using Pictest.Persistence.Storage;

namespace Pictest.Persistence.Repository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly IMongoCollection<TopicStorage> _topicCollection;
        public TopicRepository(IMongoDbRepository mongoDbRepository)
        {
            _topicCollection = mongoDbRepository.GetCollection<TopicStorage>("Pic", "Topic");
        }

        public async Task<string> CreateAsync(TopicStorage topicStorage)
        {
            await _topicCollection.InsertOneAsync(topicStorage);
            return topicStorage.Id;
        }
    }
}
