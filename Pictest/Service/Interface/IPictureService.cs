using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service.Interface
{
    public interface IPictureService
    {
        Task<CreatePictureResponse> CreateAsync(IFormFile picture, CreatePictureRequest createPictureRequest);
        Task<ReadPictureResponse> ReadAsync(string id);
        Task<ReadPictureListResponse> ReadAllAsync(string cursor, string contest);
        Task UpdateAsync(string pictureId, UpdatePictureRequest updatePictureRequest);
    }
}
