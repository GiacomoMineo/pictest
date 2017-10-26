using System.Threading.Tasks;
using Pictest.Persistence.Storage;
using Pictest.Service.Request;

namespace Pictest.Persistence.Interface
{
    public interface IPictureRepository
    {
        Task<PictureStorage> ReadAsync(string id);
        Task<string> CreateAsync(PictureStorage pictureStorage);
    }
}