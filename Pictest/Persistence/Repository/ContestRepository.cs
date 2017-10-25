using System.Threading.Tasks;
using MongoDB.Driver;
using Pictest.Persistence.Interface;
using Pictest.Persistence.Storage;

namespace Pictest.Persistence.Repository
{
    public class ContestRepository : IContestRepository
    {
        private readonly IMongoCollection<ContestStorage> _contestCollection;
        private readonly IMongoCollection<ContestSettingsStorage> _contestSettingsCollection;

        public ContestRepository(IMongoDbRepository mongoDbRepository)
        {
            _contestCollection = mongoDbRepository.GetCollection<ContestStorage>("Pic", "Contest");
            _contestSettingsCollection =
                mongoDbRepository.GetCollection<ContestSettingsStorage>("Pic", "ContestSettings");
        }

        public async Task<ContestStorage> FindByTopicAsync(string topic)
        {
            return await _contestCollection.Find(t => t.Topic == topic).Limit(1).FirstOrDefaultAsync();
        }

        public async Task<string> CreateAsync(ContestStorage contestStorage)
        {
            await _contestCollection.InsertOneAsync(contestStorage);
            return contestStorage.Id;
        }

        public async Task<ContestStorage> ReadAsync(string id) =>
            await _contestCollection.Find(t => t.Id == id).Limit(1).FirstOrDefaultAsync();

        public async Task<ContestSettingsStorage> ReadSettingsAsync(string id) => await _contestSettingsCollection
            .Find(x => x.Id == id)
            .Limit(1)
            .FirstOrDefaultAsync();

        public async Task SetCurrentAsync(string id)
        {
            var updateDefinition = Builders<ContestSettingsStorage>.Update.Set(x => x.CurrentId, id);
            await _contestSettingsCollection.FindOneAndUpdateAsync<ContestSettingsStorage>(x => x.Id == "current", updateDefinition,
                new FindOneAndUpdateOptions<ContestSettingsStorage, ContestSettingsStorage> {IsUpsert = true});
        }
    }
}