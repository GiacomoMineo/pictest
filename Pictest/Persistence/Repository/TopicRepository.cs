using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Pictest.Model;
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
            var result = await _topicCollection.Find(t => t.Name == topicStorage.Name).Limit(1).FirstOrDefaultAsync();

            if (result != null)
                throw new Exception("A Topic with the same name already exists.");

            await _topicCollection.InsertOneAsync(topicStorage);
            return topicStorage.Id;
        }

        public async Task<Topic> ReadAsync(string id)
        {
            var result = await _topicCollection.Find(t => t.Id == id).Limit(1).FirstOrDefaultAsync();

            //if (result == null)
            // not found
        }
    }
}
