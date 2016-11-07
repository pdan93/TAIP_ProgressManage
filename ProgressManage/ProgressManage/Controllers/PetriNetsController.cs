using System.Collections.Generic;
using System.Web.Http;

namespace ProgressManage.Controllers
{
    public class PetriNetsController
    {
        [HttpGet]
        public IEnumerable<object> Get()
        {
            //PetriNetsValidator.GetWarnings();
            return new List<object>();
        }
    }
}