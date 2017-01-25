using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using ProgressManage.Models;
using Workflow;

namespace ProgressManage.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StatesController : ApiController
    {
        [HttpGet]
        [LoggingAspect]
        public IEnumerable<string> Get()
        {
            return Sprint
                .GetStatesDictionary()
                .Values
                .Select(x => x.Name);
        }

        private IEnumerable<Link> CreateLinks()
        {
            var links = new[]
            {
                new Link
                {
                    Method = "GET",
                    Rel = "self",
                    Href = Url.Link("Get", new {})
                }
            };
            return links;
        }
    }
}