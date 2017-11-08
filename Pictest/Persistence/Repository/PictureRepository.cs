using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pictest.Persistence.Interface;
using Pictest.Persistence.Storage;

namespace Pictest.Persistence.Repository
{
    public class PictureRepository : IPictureRepository
    {
        private readonly IMongoCollection<PictureStorage> _pictureCollection;
        private const int PageSize = 10;

        public PictureRepository(IMongoDbRepository mongoDbRepository)
        {
            _pictureCollection = mongoDbRepository.GetCollection<PictureStorage>("Pic", "Picture");
        }

        public async Task<PictureStorage> ReadAsync(string id)
        {
            return await _pictureCollection.Find(x => x.Id == id).Limit(1).FirstOrDefaultAsync();
        }

        public async Task<List<PictureStorage>> ReadAllAsync(string cursor)
        {
            var filter = cursor != null
                ? Builders<PictureStorage>.Filter.Gt("Id", cursor)
                : Builders<PictureStorage>.Filter.Empty;

            return await _pictureCollection.Find(filter)
                .SortBy(x => x.Id)
                .Limit(PageSize).ToListAsync();
        }

        public async Task<List<PictureStorage>> ReadAllByContestAsync(string cursor, string contest)
        {
            FilterDefinition<PictureStorage> filter;
            if (cursor != null)
                filter = Builders<PictureStorage>.Filter.And(
                    Builders<PictureStorage>.Filter.Gt("Id", cursor),
                    Builders<PictureStorage>.Filter.Eq("ContestId", contest));
            else
                filter = Builders<PictureStorage>.Filter.Eq("ContestId", contest);

            return await _pictureCollection
                .Find(filter)
                .SortBy(x => x.Id)
                .Limit(PageSize).ToListAsync();
        }

        public async Task<string> CreateAsync(PictureStorage pictureStorage)
        {
            await _pictureCollection.InsertOneAsync(pictureStorage);
            return pictureStorage.Id;
        }

        public async Task UpdateAsync(string pictureId, PictureStorage pictureStorage)
        {
            var pictureUpdateBuilder = Builders<PictureStorage>.Update;

            var updateValues = new List<UpdateDefinition<PictureStorage>>();

            if (!string.IsNullOrEmpty(pictureStorage.Caption))
                updateValues.Add(pictureUpdateBuilder.Set(x => x.Caption, pictureStorage.Caption));

            updateValues.Add(pictureUpdateBuilder.Set(x => x.Votes, pictureStorage.Votes));

            var pictureUpdate = pictureUpdateBuilder.Combine(updateValues);

            await _pictureCollection.UpdateOneAsync(x => x.Id == pictureId, pictureUpdate);
        }
    }
}