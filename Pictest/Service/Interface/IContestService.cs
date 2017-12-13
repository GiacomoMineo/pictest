using System.Threading.Tasks;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Service.Interface
{
    public interface IContestService
    {
        Task<CreateContestResponse> CreateAsync(CreateContestRequest contest);
        Task<ReadContestResponse> ReadAsync(string id);
        Task<ReadContestResponse> ReadCurrentAsync();
        Task<ReadContestResponse> SetCurrentAsync(string id);
        Task UpdateAsync(string id, UpdateContestRequest updateContestRequest);
        Task<ReadContestListResponse> ReadAllAsync(string cursor);
    }
}
