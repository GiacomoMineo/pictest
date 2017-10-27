using System.Threading.Tasks;
using MongoDB.Driver;
using Pictest.Model;
using Pictest.Persistence.Interface;
using Pictest.Persistence.Storage;

namespace Pictest.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserStorage> _userCollection;

        public UserRepository(IMongoDbRepository mongoDbRepository)
        {
            _userCollection = mongoDbRepository.GetCollection<UserStorage>("Pic", "User");
        }
        public async Task<UserStorage> FindByEmailAsync(string email)
        {
            return await _userCollection.Find(x => x.Email == email).Limit(1).FirstOrDefaultAsync();
        }

        public async Task<bool> CheckPasswordSignInAsync(User user, string password)
        {
            return true;
        }

        public async Task<string> CreateAsync(UserStorage user)
        {
            await _userCollection.InsertOneAsync(user);
            return user.Id;
        }
    }
}
