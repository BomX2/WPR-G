using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace WebProjectG.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<()>>PostTest()
        {
            //_context.Test.Add();
            //await _context.SaveChangesAsync()

            return CreatedAtAction();
        }
    }
}
