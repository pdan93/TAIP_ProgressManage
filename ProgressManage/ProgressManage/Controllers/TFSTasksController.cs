using System;
using System.Web.Http;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Cors;
using Workflow.Repositories;

namespace ProgressManage.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TfsTasksController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            NetworkCredential credential;
            var uri = new Uri("https://alexandrurusu93.visualstudio.com/DefaultCollection");
            var teamProjectCollection = TfsRepository.GetTeamProjectCollection(uri, out credential);
            return TfsRepository.GetWorkItems(teamProjectCollection)
                .Where(item => item.Type.Name == "Task" && item.AreaPath.Contains("Progress"))
                .Select(item => item.Title);
        }
    }
}
