using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace healthcare_visuzlier25.Controllers
{
    [Route("api/hello")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        [Route("mohamed")]
        public string SayHello()
        {
            return "Hello World!";
        }
    }
}
