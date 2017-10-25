using System;
using Pictest.Persistence.Storage;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service.Mapper
{
    public static class ContestMapper
    {
        public static ContestStorage MapCreateContestRequestToContestStorage(CreateContestRequest createContestRequest)
        {
            if (createContestRequest == null)
                return null;

            return new ContestStorage
            {
                CreatedAt = DateTime.UtcNow,
                Topic = createContestRequest.Topic
            };
        }

        public static ReadContestResponse MapContestStorageToReadContestResponse(ContestStorage contestStorage)
        {
            if (contestStorage == null)
                return null;

            return new ReadContestResponse
            {
                Id = contestStorage.Id,
                CreatedAt = contestStorage.CreatedAt,
                Topic = contestStorage.Topic
            };
        }
    }
}
