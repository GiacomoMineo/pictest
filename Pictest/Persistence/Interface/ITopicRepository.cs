using System.Threading.Tasks;
using Pictest.Persistence.Storage;

namespace Pictest.Persistence.Interface
{
    public interface ITopicRepository
    {
        Task<string> CreateAsync(TopicStorage topicStorage);
    }
}
