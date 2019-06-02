using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Akeem.ChangRead.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        public TestController(IConfiguration configuration) : base(configuration)
        {
        }
        // GET api/values
        [HttpGet]
        [Authorize]//添加Authorize标签，可以加在方法上，也可以加在类上
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        //[Authorize(Roles = "admin")]
        //[HttpPost]
        //public IActionResult Post()
        //{
        //    return Json("Post");
        //}

        //[Authorize(Roles = "administrator" )]
        //[HttpPut]
        //public IActionResult Put()
        //{
        //    return Json("Put");
        //}

    }
}