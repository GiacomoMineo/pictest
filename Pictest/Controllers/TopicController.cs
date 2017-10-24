using System;
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
        public async Task<IActionResult> Get(string id)
        {
            var result = await _topicRepository.ReadAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var result = await _topicRepository.ReadLatestAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Topic request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(new TopicPostResponse
            {
                Id = await _topicRepository.CreateAsync(new TopicStorage
                {
                    Name = request.Name,
                    CreatedAt = DateTime.UtcNow,
                    Tags = request.Tags
                })
            });
        }
    }
}