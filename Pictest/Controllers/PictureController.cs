using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pictest.Service.Interface;
using Pictest.Service.Request;

namespace Pictest.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [Consumes("application/json", "multipart/form-data")]
    [Produces("application/json")]
    public class PictureController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _pictureService.ReadAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInContest([FromQuery] string cursor, string contest)
        {
            var pictures = await _pictureService.ReadAllAsync(cursor, contest);

            return Ok(pictures);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePictureRequest updatePictureRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userId = HttpContext?.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            await _pictureService.UpdateAsync(id, userId, updatePictureRequest);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile picture, [FromForm] CreatePictureRequest createPictureRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userId = HttpContext?.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var result = await _pictureService.CreateAsync(picture, userId, createPictureRequest);

            return Ok(result);
        }
    }
}