using System;
using System.Web.Http;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Workflow.Repositories;

namespace ProgressManage.Controllers
{
    public class TfsTasksController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            NetworkCredential credential;
            var uri = new Uri("https://alexandrurusu93.visualstudio.com/DefaultCollection");
            var teamProjectCollection = TfsRepository.GetTeamProjectCollection(uri, out credential);
            return TfsRepository.GetWorkItems(teamProjectCollection)
                .Select(item => item.Title);
        }
    }
}
