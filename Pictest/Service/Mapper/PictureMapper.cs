using System;
using System.Collections.Generic;
using System.Linq;
using Pictest.Persistence.Storage;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service.Mapper
{
    public static class PictureMapper
    {
        public static PictureStorage MapCreatePictureRequestToPictureStorage(CreatePictureRequest createPictureRequest,
            string url, string userId)
        {
            if (createPictureRequest == null)
                return null;

            return new PictureStorage
            {
                CreatedAt = DateTime.UtcNow,
                Caption = createPictureRequest.Caption,
                ContestId = createPictureRequest.ContestId,
                Url = url,
                UserId = userId,
                Votes = 0
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
                Caption = pictureStorage.Caption,
                CreatedAt = pictureStorage.CreatedAt,
                Votes = pictureStorage.Votes
            };
        }

        public static ReadPictureListResponse MapPictureStorageListToReadPictureResponseList(
            List<PictureStorage> pictureStorages)
        {
            var pictures = pictureStorages?.Select(MapPictureStorageToReadPictureResponse).ToList();

            return new ReadPictureListResponse
            {
                Pictures = pictures,
                Cursor = pictures?.LastOrDefault()?.Id
            };
        }

        public static PictureStorage MapUpdatePictureRequestToPictureStorage(UpdatePictureRequest updatePictureRequest)
        {
            if (updatePictureRequest == null)
                return null;

            return new PictureStorage {Caption = updatePictureRequest.Caption};
        }
    }
}