using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fake.API.Controllers
{
    [Route("api/handAPI")]
    public class HandAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1swsdwqedqwdqwdqwdqwdwq", "value2" };
        }
    }
}


        
