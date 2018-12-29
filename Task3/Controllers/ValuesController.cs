using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Task3.Controllers
{
    [Route("api/[controller]"), Authorize, RequireHttps]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var data = new
            {
                status = "You are now authorized",
                value = "This some highly confidential data."
            };

            return Ok(data);
        }
    }
}
