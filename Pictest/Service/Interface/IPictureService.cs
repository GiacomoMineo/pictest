using System.Threading.Tasks;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service.Interface
{
    public interface IPictureService
    {
        Task<CreatePictureResponse> CreateAsync(CreatePictureRequest picture);
        Task<ReadPictureResponse> ReadAsync(string id);
    }
}
