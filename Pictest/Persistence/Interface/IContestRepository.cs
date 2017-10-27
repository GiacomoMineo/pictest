using System.Threading.Tasks;
using MongoDB.Bson;
using Pictest.Persistence.Storage;

namespace Pictest.Persistence.Interface
{
    public interface IContestRepository
    {
        Task<ContestStorage> FindByTopicAsync(string topic);
        Task<string> CreateAsync(ContestStorage contestStorage);
        Task<ContestStorage> ReadAsync(string id);
        Task<ContestSettingsStorage> ReadSettingsAsync(string id);
        Task SetCurrentAsync(string id);
        Task UpdateAsync(string id, ContestStorage contestStorage);
    }
}
