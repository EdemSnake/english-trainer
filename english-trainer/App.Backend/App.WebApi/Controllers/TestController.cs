using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace App.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("texts")]
        public ActionResult<IEnumerable<object>> GetTextExamples()
        {
            var texts = new List<object>
            {
                new { id = 1, title = "The quick brown fox jumps over the lazy dog." },
                new { id = 2, title = "Never underestimate the power of a good book." },
                new { id = 3, title = "The early bird catches the worm." },
                new { id = 4, title = "Practice makes perfect." }
            };
            return Ok(texts);
        }
    }
}
