using System.Collections.Generic;
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
        private const int PageSize = 1;

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
            await _contestSettingsCollection.FindOneAndUpdateAsync<ContestSettingsStorage>(x => x.Id == "current",
                updateDefinition,
                new FindOneAndUpdateOptions<ContestSettingsStorage, ContestSettingsStorage> {IsUpsert = true});
        }

        public async Task SetWinnerAsync(string id)
        {
            var updateDefinition = Builders<ContestSettingsStorage>.Update.Set(x => x.WinnerId, id);
            await _contestSettingsCollection.FindOneAndUpdateAsync<ContestSettingsStorage>(x => x.Id == "winner",
                updateDefinition,
                new FindOneAndUpdateOptions<ContestSettingsStorage, ContestSettingsStorage> {IsUpsert = true});
        }

        public async Task<List<ContestStorage>> ReadAllAsync(string cursor)
        {
            var filter = cursor != null
                ? Builders<ContestStorage>.Filter.Gt("Id", cursor)
                : Builders<ContestStorage>.Filter.Empty;

            return await _contestCollection.Find(filter)
                .SortBy(x => x.Id)
                .Limit(PageSize).ToListAsync();
        }

        public async Task UpdateAsync(string id, ContestStorage contestStorage)
        {
            var contestUpdateBuilder = Builders<ContestStorage>.Update;

            var updateValues = new List<UpdateDefinition<ContestStorage>>();

            if (contestStorage.Voters != null)
                updateValues.Add(contestUpdateBuilder.Set(x => x.Voters, contestStorage.Voters));
            if (contestStorage.Closed != null)
                updateValues.Add(contestUpdateBuilder.Set(x => x.Closed, contestStorage.Closed));
            if (contestStorage.Winner != null)
                updateValues.Add(contestUpdateBuilder.Set(x => x.Winner, contestStorage.Winner));

            var pictureUpdate = contestUpdateBuilder.Combine(updateValues);

            await _contestCollection.UpdateOneAsync(x => x.Id == id, pictureUpdate);
        }
    }
}