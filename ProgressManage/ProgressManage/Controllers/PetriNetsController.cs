using System.Web.Http;
using PetriNets;
using Workflow;
using Workflow.SprintEntities;

namespace ProgressManage.Controllers
{
    public class PetriNetsController : ApiController
    {
        [HttpGet]
        [LoggingAspect]
        public string Get()
        {
            var task = new Task(new State(StatesDictionary.Start));

            Sprint sprint = new Sprint();
            sprint.CreateNetwork(task);

            if (sprint.IsHistoryValid(task))
            {
                return "History of task passed the test";
            }

            return "History of task not passed the test";
        }

        [HttpPost]
        [LoggingAspect]
        public string Post([FromBody] string text)
        {
            return text;
        }
    }
}