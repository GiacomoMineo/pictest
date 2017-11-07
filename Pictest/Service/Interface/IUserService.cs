using System.Threading.Tasks;
using Pictest.Model;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service.Interface
{
    public interface IUserService
    {
        Task<User> FindByEmailAsync(string email);
        Task<bool> CheckPasswordSignInAsync(User user, string password);
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest registerRequest);
    }
}
