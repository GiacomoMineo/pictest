using Microsoft.AspNetCore.Mvc;

namespace Pictest.Controllers
{
    [Route("/api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PictureController : ControllerBase
    {
    }
}
