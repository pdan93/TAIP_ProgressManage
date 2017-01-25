using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PetriNets;
using Workflow;
using Workflow.SprintEntities;
using System.Web.Hosting;
using System.Web.Http.Cors;
using ProgressManage.Models;

namespace ProgressManage.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PetriNetsController : ApiController
    {
        private readonly string _revisionsFilePath = HostingEnvironment.MapPath(@"~/App_Data/taskRevisionsValid.json");
        private readonly string _transitionsDataPath = HostingEnvironment.MapPath(@"~/App_Data/transitions.json");

        [HttpGet]
        [LoggingAspect]
        public string Get(string path = null, string transitionsDataPath = null)
        {
            if (path == null)
            {
                path = _revisionsFilePath;
            }

            if (transitionsDataPath == null)
            {
                transitionsDataPath = _transitionsDataPath;
            }

            DataHandler dataHandler = new DataHandler();
            var taskRevions = dataHandler.ImportTaskRevisions(path).Skip(1);
            var task = new Task(new State(StatesNames.Start));

            Sprint sprint = new Sprint();
            sprint.CreateNetwork(task, transitionsDataPath);
            return sprint.IsHistoryValid(task, taskRevions);
        }

        private IEnumerable<Link> CreateLinks()
        {
            var links = new[]
            {
                new Link
                {
                    Method = "GET",
                    Rel = "self",
                    Href = Url.Link("Get", new { path = "taskRevisionsValid.json", transitionsDataPath = "transitions"})
                }
            };
            return links;
        }
    }
}