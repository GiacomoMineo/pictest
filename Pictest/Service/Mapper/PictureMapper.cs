using System;
using Pictest.Persistence.Storage;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service.Mapper
{
    public static class PictureMapper
    {
        public static PictureStorage MapCreatePictureRequestToPictureStorage(CreatePictureRequest createPictureRequest)
        {
            if (createPictureRequest == null)
                return null;

            return new PictureStorage
            {
                CreatedAt = DateTime.UtcNow,
                Caption = createPictureRequest.Caption
            };
        }

        public static ReadPictureResponse MapPictureStorageToReadPictureResponse(PictureStorage pictureStorage)
        {
            if (pictureStorage == null)
                return null;

            return new ReadPictureResponse
            {
                Id = pictureStorage.Id,
                Url = pictureStorage.Url,
                Caption = pictureStorage.Caption
            };
        }
    }
}
