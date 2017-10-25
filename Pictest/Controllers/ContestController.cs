using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pictest.Service.Interface;
using Pictest.Service.Request;

namespace Pictest.Controllers
{
    [Route("/api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ContestController : ControllerBase
    {
        private readonly IContestService _contestService;

        public ContestController(IContestService contestService)
        {
            _contestService = contestService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _contestService.ReadAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent()
        {
            var result = await _contestService.ReadCurrentAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}/current")]
        public async Task<IActionResult> SetCurrent(string id)
        {
            var result = await _contestService.SetCurrentAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContestRequest createContestRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(await _contestService.CreateAsync(createContestRequest));
        }
    }
}