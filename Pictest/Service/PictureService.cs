using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Pictest.Persistence.Interface;
using Pictest.Service.Interface;
using Pictest.Service.Mapper;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;

        public PictureService(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public async Task<CreatePictureResponse> CreateAsync(CreatePictureRequest createPictureRequest)
        {
            var result = await _pictureRepository.CreateAsync(
                PictureMapper.MapCreatePictureRequestToPictureStorage(createPictureRequest));

            return new CreatePictureResponse {Id = result};
        }

        public async Task<ReadPictureResponse> ReadAsync(string id)
        {
            return PictureMapper.MapPictureStorageToReadPictureResponse(await _pictureRepository.ReadAsync(id));
        }
    }
}