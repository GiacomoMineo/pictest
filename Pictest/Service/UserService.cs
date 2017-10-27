using System.Threading.Tasks;
using Pictest.Model;
using Pictest.Persistence.Interface;
using Pictest.Service.Interface;

namespace Pictest.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<User> FindByEmailAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CheckPasswordSignInAsync(User user, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
