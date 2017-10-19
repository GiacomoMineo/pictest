using Pictest.Persistence.Interface;

namespace Pictest.Persistence.Repository
{
    public class TopicRepository : ITopicRepository
    {
        public TopicRepository(IMongoDbRepository mongoDbRepository)
        {
            
        }
    }
}
