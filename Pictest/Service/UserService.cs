using System;
using System.Threading.Tasks;
using Pictest.Model;
using Pictest.Persistence.Interface;
using Pictest.Service.Interface;
using Pictest.Service.Mapper;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return UserMapper.MapUserStorageToUser(await _userRepository.FindByEmailAsync(email));
        }

        public async Task<bool> CheckPasswordSignInAsync(User user, string password)
        {
            if (user.Password == password)
                return true;

            return false;
        }

        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            var exists = await _userRepository.FindByEmailAsync(createUserRequest.Email);

            if (exists != null)
                throw new Exception("A user with the same Email already exists.");

            var result =
                await _userRepository.CreateAsync(UserMapper.MapCreateUserRequestToUserStorage(createUserRequest));

            return new CreateUserResponse {Id = result};
        }
    }
}