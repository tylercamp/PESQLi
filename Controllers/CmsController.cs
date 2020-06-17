using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PESQLi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CmsController : ControllerBase
    {
        private ModelContext context;

        public CmsController(ModelContext context)
        {
            this.context = context;
        }

        [HttpGet("{*tag}")]
        public IActionResult GetContent(String tag)
        {
            var result = context.CmsEntries.FromSqlRaw($@"SELECT * FROM cms_entries WHERE tag = '{tag}'").FirstOrDefault();

            if (result == null) return NotFound();
            else return Ok(result);
        }
    }
}
