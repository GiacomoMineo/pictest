using System.Threading.Tasks;
using Pictest.Model;

namespace Pictest.Service.Interface
{
    public interface IUserService
    {
        Task<User> FindByEmailAsync(string email);
        Task<bool> CheckPasswordSignInAsync(User user, string password);
    }
}
