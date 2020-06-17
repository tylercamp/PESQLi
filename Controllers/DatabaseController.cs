using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PESQLi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private ModelContext context;
        private ILogger<DatabaseController> logger;

        public DatabaseController(ModelContext context, ILogger<DatabaseController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                numUsers = context.Users.Count(),
                numEntries = context.CmsEntries.Count()
            });
        }

        [HttpGet("reset")]
        public IActionResult ResetDB()
        {
            if (!context.Database.EnsureDeleted())
                return Problem("Database.EnsureDeleted returned false");

            if (!context.Database.EnsureCreated())
                return Problem("Database.EnsureCreated returned false");

            ModelContext.Seed.Apply(context);

            return Ok();
        }
    }
}
