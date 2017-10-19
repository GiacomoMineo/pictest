using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pictest.Model;

namespace Pictest.Controllers
{
    [Route("/api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class TopicController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return id;
        }

        [HttpPost]
        public Topic Post([FromBody] Topic request)
        {
            return request;
        }

        [HttpGet]
        public List<Topic> Index()
        {
            return new List<Topic>();
        }
    }
}
