using System.Threading.Tasks;
using Pictest.Model;
using Pictest.Persistence.Storage;

namespace Pictest.Persistence.Interface
{
    public interface IUserRepository
    {
        Task<UserStorage> FindByEmailAsync(string email);
        Task<bool> CheckPasswordSignInAsync(User user, string password);
        Task<string> CreateAsync(UserStorage user);
    }
}
