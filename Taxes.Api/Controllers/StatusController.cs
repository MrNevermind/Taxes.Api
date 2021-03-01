using Microsoft.AspNetCore.Mvc;

namespace Taxes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        [HttpGet]
        public string Status()
        {
            return "OK";
        }
    }
}
