using System.Threading.Tasks;
using MongoDB.Driver;
using Pictest.Persistence.Interface;
using Pictest.Persistence.Storage;

namespace Pictest.Persistence.Repository
{
    public class PictureRepository : IPictureRepository
    {
        private readonly IMongoCollection<PictureStorage> _pictureCollection;
        public PictureRepository(IMongoDbRepository mongoDbRepository)
        {
            _pictureCollection = mongoDbRepository.GetCollection<PictureStorage>("Pic", "Picture");
        }
        public async Task<PictureStorage> ReadAsync(string id)
        {
            return await _pictureCollection.Find(x => x.Id == id).Limit(1).FirstOrDefaultAsync();
        }

        public async Task<string> CreateAsync(PictureStorage pictureStorage)
        {
            await _pictureCollection.InsertOneAsync(pictureStorage);
            return pictureStorage.Id;
        }
    }
}
