using System;
using System.Collections.Generic;
using System.Linq;
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

        public static ContestStorage MapUpdateContestRequestToContestStorage(UpdateContestRequest updateContestRequest)
        {
            if (updateContestRequest == null)
                return null;

            return new ContestStorage
            {
                Closed = updateContestRequest.Closed
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
                Topic = contestStorage.Topic,
                Closed = contestStorage.Closed
            };
        }

        public static ReadContestListResponse MapContestStorageToReadContestResponseList(List<ContestStorage> contestStorages)
        {
            var contests = contestStorages?.Select(MapContestStorageToReadContestResponse).ToList();

            return new ReadContestListResponse
            {
                Contests = contests,
                Cursor = contests?.LastOrDefault()?.Id
            };
        }
    }
}
