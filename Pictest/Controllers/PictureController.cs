using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pictest.Persistence.Interface;

namespace Pictest.Controllers
{
    [Route("/api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PictureController : ControllerBase
    {
        private readonly IPictureRepository _pictureRepository;
        public PictureController(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _pictureRepository.ReadAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
