using Pictest.Model;
using Pictest.Persistence.Storage;
using Pictest.Service.Request;

namespace Pictest.Service.Mapper
{
    public static class UserMapper
    {
        public static UserStorage MapCreateUserRequestToUserStorage(CreateUserRequest createUserRequest)
        {
            if (createUserRequest == null)
                return null;

            return new UserStorage
            { 
                Email = createUserRequest.Email,
                Password = createUserRequest.Password
            };
        }

        public static User MapUserStorageToUser(UserStorage userStorage)
        {
            if (userStorage == null)
                return null;

            return new User
            {
                Id = userStorage.Id,
                Email = userStorage.Email,
                Password = userStorage.Password
            };
        }
    }
}
