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
        private readonly string _transitionPath = HostingEnvironment.MapPath(@"~/App_Data/transitions.json");

        [HttpGet]
        [LoggingAspect]
        public string Get(string path = null, string transitionPath = null)
        {
            if (path == null)
            {
                path = _revisionsFilePath;
            }

            if (transitionPath == null)
            {
                transitionPath = _transitionPath;
            }

            DataHandler dataHandler = new DataHandler();
            var taskRevions = dataHandler.ImportTaskRevisions(path).Skip(1);
            var task = new Task(new State(StatesNames.Start));

            Sprint sprint = new Sprint();
            sprint.CreateNetwork(task, transitionPath);
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