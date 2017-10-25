using System.Threading.Tasks;
using Pictest.Persistence.Storage;

namespace Pictest.Persistence.Interface
{
    public interface IPictureRepository
    {
        Task<PictureStorage> ReadAsync(string id);
    }
}