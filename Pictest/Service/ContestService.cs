using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chroniton;
using Chroniton.Jobs;
using Chroniton.Schedules;
using Pictest.Persistence.Interface;
using Pictest.Service.Interface;
using Pictest.Service.Mapper;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service
{
    public class ContestService : IContestService
    {
        private readonly IContestRepository _contestRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly ISingularity _singularity;

        public ContestService(IContestRepository contestRepository, IPictureRepository pictureRepository, ISingularity singularity)
        {
            _contestRepository = contestRepository;
            _pictureRepository = pictureRepository;
            _singularity = singularity;
        }

        public async Task<CreateContestResponse> CreateAsync(CreateContestRequest contest)
        {
            var exists = await _contestRepository.FindByTopicAsync(contest.Topic);

            if (exists != null)
                throw new Exception("A contest with the same Topic already exists.");

            var result =
                await _contestRepository.CreateAsync(ContestMapper.MapCreateContestRequestToContestStorage(contest));

            if (contest.Current)
            {
                await _contestRepository.SetCurrentAsync(result);

                // Schedule contest closing job
                var job = new SimpleJob(async scheduledTime =>
                {
                    await UpdateAsync(result, new UpdateContestRequest
                    {
                        Closed = true
                    });
                });

                _singularity.ScheduleJob(new RunOnceSchedule(TimeSpan.FromMinutes(1)), job, false);
            }

            return new CreateContestResponse {Id = result};
        }

        public async Task<ReadContestListResponse> ReadAllAsync(string cursor)
        {
            return ContestMapper.MapContestStorageToReadContestResponseList(await _contestRepository.ReadAllAsync(cursor));
        }

        public async Task<ReadContestResponse> ReadAsync(string id)
        {
            return ContestMapper.MapContestStorageToReadContestResponse(await _contestRepository.ReadAsync(id));
        }

        public async Task<ReadContestResponse> ReadCurrentAsync()
        {
            var currentContest = await _contestRepository.ReadSettingsAsync("current");

            if (currentContest == null)
                return null;
            
            return ContestMapper.MapContestStorageToReadContestResponse(await _contestRepository.ReadAsync(currentContest.CurrentId));
        }

        public async Task<ReadContestResponse> SetCurrentAsync(string id)
        {
            var result = await _contestRepository.ReadAsync(id);

            if (result != null)
                await _contestRepository.SetCurrentAsync(id);

            return ContestMapper.MapContestStorageToReadContestResponse(result);
        }

        public async Task UpdateAsync(string id, UpdateContestRequest updateContestRequest)
        {
            var contestStorage = ContestMapper.MapUpdateContestRequestToContestStorage(updateContestRequest);

            if (updateContestRequest.Closed == true)
            {
                var winner = await DeclareContestWinnerAsync(id);
                contestStorage.Winner = winner;
                await _contestRepository.SetWinnerAsync(winner);
            }

            await _contestRepository.UpdateAsync(id, contestStorage);
        }

        public async Task<string> DeclareContestWinnerAsync(string id)
        {
            return await _pictureRepository.ReadPictureUserWithMostVotes(id);
        }
    }
}