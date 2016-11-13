using System.Collections.Generic;
using System.Web.Http;

namespace ProgressManage.Controllers
{
    public class PetriNetsController : ApiController
    {
        [HttpGet]
        [LoggingAspect]
        public IEnumerable<string> Get()
        {
            return new List<string>()
            {
                "Nets"
            };
        }
    }
}