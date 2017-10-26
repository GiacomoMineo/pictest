using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pictest.Persistence.Interface;
using Pictest.Service.Interface;
using Pictest.Service.Request;

namespace Pictest.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile picture, [FromForm] CreatePictureRequest createPictureRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var filePath = Path.GetFullPath(Environment.CurrentDirectory + "/Uploads/" + picture.FileName);

            if (picture.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await picture.CopyToAsync(stream);

            var result = await _pictureService.CreateAsync(createPictureRequest);

            return Ok(result);
        }
    }
}