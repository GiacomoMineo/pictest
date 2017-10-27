using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pictest.Persistence.Interface;
using Pictest.Persistence.Storage;
using Pictest.Service.Interface;
using Pictest.Service.Mapper;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IContestRepository _contestRepository;

        public PictureService(IPictureRepository pictureRepository, IContestRepository contestRepository)
        {
            _pictureRepository = pictureRepository;
            _contestRepository = contestRepository;
        }

        public async Task<CreatePictureResponse> CreateAsync(IFormFile picture, CreatePictureRequest createPictureRequest)
        {
            var filePath = Path.GetFullPath(Environment.CurrentDirectory + "/Uploads/" + picture.FileName);

            if (picture.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await picture.CopyToAsync(stream);

            string userId = null; // TODO read from session

            var result = await _pictureRepository.CreateAsync(
                PictureMapper.MapCreatePictureRequestToPictureStorage(createPictureRequest, picture.FileName, userId));

            return new CreatePictureResponse {Id = result};
        }

        public async Task<ReadPictureResponse> ReadAsync(string id)
        {
            return PictureMapper.MapPictureStorageToReadPictureResponse(await _pictureRepository.ReadAsync(id));
        }

        public async Task<ReadPictureListResponse> ReadAllAsync(string cursor, string contest)
        {
            if (contest != null)
                return PictureMapper.MapPictureStorageListToReadPictureResponseList(
                    await _pictureRepository.ReadAllByContestAsync(cursor, contest));

            return PictureMapper.MapPictureStorageListToReadPictureResponseList(
                await _pictureRepository.ReadAllAsync(cursor));
        }

        public async Task UpdateAsync(string pictureId, UpdatePictureRequest updatePictureRequest)
        {
            var pictureStorage = PictureMapper.MapUpdatePictureRequestToPictureStorage(updatePictureRequest);

            var previousPicture = await _pictureRepository.ReadAsync(pictureId);
            var pictureContest = await _contestRepository.ReadAsync(previousPicture.ContestId);

            string userId = null; // TODO read from session
            if (updatePictureRequest.Vote && pictureContest.Voters.All(x => x != userId))
            {
                pictureContest.Voters.Add(userId);
                pictureStorage.Votes = previousPicture.Votes + 1;

                await _contestRepository.UpdateAsync(pictureContest.Id, new ContestStorage
                {
                    Voters = pictureContest.Voters
                });
            }

            await _pictureRepository.UpdateAsync(pictureId, pictureStorage);
        }
    }
}