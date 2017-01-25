using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using ProgressManage.Models;
using Workflow;

namespace ProgressManage.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RevisionsController : ApiController
    {
        private readonly string _revisionsFilePath = HostingEnvironment.MapPath(@"~/App_Data/taskRevisionsValid.json");

        [HttpGet]
        [LoggingAspect]
        public IEnumerable<Revision> Get()
        {
            return new DataHandler()
                .ImportTaskRevisions(_revisionsFilePath);
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