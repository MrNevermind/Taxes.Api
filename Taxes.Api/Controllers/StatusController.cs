using Microsoft.AspNetCore.Mvc;

namespace Taxes.Api.Controllers
{
    public class StatusController : Controller
    {
        [HttpGet]
        public string Status()
        {
            return "OK";
        }
    }
}
