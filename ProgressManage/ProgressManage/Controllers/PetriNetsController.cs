using System.Linq;
using System.Web.Http;
using PetriNets;
using Workflow;
using Workflow.SprintEntities;

namespace ProgressManage.Controllers
{
    public class PetriNetsController : ApiController
    {
        private const string RevionsFilePath = "E:\\Informatica\\Master\\Advanced Software Engineering Techniques\\ProgressManage\\TestFiles\\taskRevisions.json";

        [HttpGet]
        [LoggingAspect]
        public string Get(string path = null)
        {
            if (path == null)
            {
                path = RevionsFilePath;
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