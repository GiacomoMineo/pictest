using System;
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
            TopicStorage result = await _topicCollection.Find(t => t.Name == topicStorage.Name).Limit(1)
                .FirstOrDefaultAsync();

            if (result != null)
                throw new Exception("A Topic with the same name already exists.");

            await _topicCollection.InsertOneAsync(topicStorage);
            return topicStorage.Id;
        }

        public async Task<TopicStorage> ReadAsync(string id) =>
            await _topicCollection.Find(t => t.Id == id).Limit(1).FirstOrDefaultAsync();

        public async Task<TopicStorage> ReadLatestAsync() => await _topicCollection
            .Find(x => true)
            .Sort(Builders<TopicStorage>.Sort.Descending(x => x.Id))
            .Limit(1)
            .FirstOrDefaultAsync();
    }
}