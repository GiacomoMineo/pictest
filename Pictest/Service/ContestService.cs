using System;
using System.Threading.Tasks;
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

        public ContestService(IContestRepository contestRepository)
        {
            _contestRepository = contestRepository;
        }

        public async Task<CreateContestResponse> CreateAsync(CreateContestRequest contest)
        {
            var exists = await _contestRepository.FindByTopicAsync(contest.Topic);

            if (exists != null)
                throw new Exception("A contest with the same Topic already exists.");

            var result =
                await _contestRepository.CreateAsync(ContestMapper.MapCreateContestRequestToContestStorage(contest));

            return new CreateContestResponse {Id = result};
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
    }
}