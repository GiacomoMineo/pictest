using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pictest.Model;
using Pictest.Model.Response;
using Pictest.Persistence.Interface;
using Pictest.Persistence.Storage;

namespace Pictest.Controllers
{
    [Route("/api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;

        public TopicController(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        [HttpGet("{id}")]
        public async Task<Topic> Get(string id)
        {
            return await _topicRepository.ReadAsync(id);
        }

        [HttpPost]
        public async Task<TopicPostResponse> Post([FromBody] Topic request)
        {
            return new TopicPostResponse
            {
                Id = await _topicRepository.CreateAsync(new TopicStorage
                {
                    Name = request.Name,
                    CreatedAt = request.CreatedAt,
                    Tags = request.Tags
                })
            };
        }

        [HttpGet]
        public List<Topic> Index()
        {
            return new List<Topic>();
        }
    }
}