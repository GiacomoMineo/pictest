using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pictest.Persistence.Interface;
using Pictest.Service.Interface;
using Pictest.Service.Request;

namespace Pictest.Controllers
{
    [Route("/api/[controller]")]
    [Consumes("application/json")]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePictureRequest createPictureRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _pictureService.CreateAsync(createPictureRequest));
        }
    }
}
