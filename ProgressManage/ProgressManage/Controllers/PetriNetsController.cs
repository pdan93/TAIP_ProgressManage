using System.Linq;
using System.Web.Http;
using PetriNets;
using Workflow;
using Workflow.SprintEntities;
using System.Web.Hosting;

namespace ProgressManage.Controllers
{
    public class PetriNetsController : ApiController
    {
        private readonly string _revisionsFilePath = HostingEnvironment.MapPath(@"~/App_Data/taskRevisions.json");

        [HttpGet]
        [LoggingAspect]
        public string Get(string path = null)
        {
            if (path == null)
            {
                path = _revisionsFilePath;
            }
            DataHandler dataHandler = new DataHandler();
            var taskRevions = dataHandler.ImportTaskRevisions(path).Skip(1);
            var task = new Task(new State(StatesDictionary.Start));

            Sprint sprint = new Sprint();
            sprint.CreateNetwork(task);
            return sprint.IsHistoryValid(task, taskRevions);
        }

        [HttpPost]
        [LoggingAspect]
        public string Post([FromBody] string text)
        {
            return text;
        }
    }
}