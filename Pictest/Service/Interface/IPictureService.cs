using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service.Interface
{
    public interface IPictureService
    {
        Task<CreatePictureResponse> CreateAsync(IFormFile picture, string userId, CreatePictureRequest createPictureRequest);
        Task<ReadPictureResponse> ReadAsync(string id);
        Task<ReadPictureListResponse> ReadAllAsync(string cursor, string contest);
        Task UpdateAsync(string pictureId, string userId, UpdatePictureRequest updatePictureRequest);
    }
}
