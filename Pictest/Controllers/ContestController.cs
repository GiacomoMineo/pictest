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

        [HttpGet]
        public async Task<IActionResult> ReadAll([FromQuery] string cursor)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _contestService.ReadAllAsync(cursor);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _contestService.ReadAsync(id);

            if (result == null)
                return NotFound();
            
            return Ok(result);
        }

        [HttpGet("current")]
        public async Task<IActionResult> ReadCurrent()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _contestService.ReadCurrentAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}/current")]
        public async Task<IActionResult> UpdateCurrent(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _contestService.SetCurrentAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateContestRequest updateContestRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _contestService.UpdateAsync(id, updateContestRequest);

            return Ok();
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