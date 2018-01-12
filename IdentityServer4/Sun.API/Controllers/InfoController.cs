using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Sun.API.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class InfoController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Json(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
