using System;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Web.Http;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace ProgressManage.Controllers
{
    public class TFSTasksController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Uri collectionUri = new Uri("https://alexandrurusu93.visualstudio.com/DefaultCollection");
            var user = ConfigurationManager.AppSettings["TfsClientUser"];
            var password = ConfigurationManager.AppSettings["TfsClientPassword"];
            NetworkCredential credential = new NetworkCredential(user, password);
            BasicAuthCredential basicCred = new BasicAuthCredential(credential);
            TfsClientCredentials tfsCred = new TfsClientCredentials(basicCred);
            tfsCred.AllowInteractive = false;
            TfsTeamProjectCollection teamProjectCollection = new TfsTeamProjectCollection(collectionUri, tfsCred);
            teamProjectCollection.Authenticate();

            WorkItemStore workItemStore = teamProjectCollection.GetService<WorkItemStore>();
            WorkItemCollection workItemCollection = workItemStore.Query(
                "SELECT [System.Id], [System.WorkItemType]," + 
                "[System.State], [System.AssignedTo]," +
                "[System.Title] FROM WorkItems");
            IEnumerable<string> ids = workItemCollection
                .OfType<WorkItem>()
                .Select(item => item.Title);
            return ids;
        }


    }
}
