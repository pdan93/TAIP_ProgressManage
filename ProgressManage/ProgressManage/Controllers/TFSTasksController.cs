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
    public class TfsTasksController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            NetworkCredential credential;
            var teamProjectCollection = GetTeamProjectCollection(new Uri("https://alexandrurusu93.visualstudio.com/DefaultCollection"),
                ConfigurationManager.AppSettings["TfsClientUser"],
                ConfigurationManager.AppSettings["TfsClientPassword"],
                out credential);
            return GetWorkItems(teamProjectCollection)
                .Select(item => item.Title);
        }

        private static IEnumerable<WorkItem> GetWorkItems(TfsConnection teamProjectCollection)
        {
            WorkItemStore workItemStore = teamProjectCollection.GetService<WorkItemStore>();
            WorkItemCollection workItemCollection = workItemStore.Query(
                "SELECT [System.Id], [System.WorkItemType]," +
                "[System.State], [System.AssignedTo]," +
                "[System.Title] FROM WorkItems");
            return workItemCollection
                .OfType<WorkItem>(); ;
        }

        private static TfsTeamProjectCollection GetTeamProjectCollection(Uri collectionUri, string user,
            string password, out NetworkCredential credential)
        {
            credential = new NetworkCredential(user, password);
            BasicAuthCredential basicCred = new BasicAuthCredential(credential);
            TfsClientCredentials tfsCred = new TfsClientCredentials(basicCred);
            tfsCred.AllowInteractive = false;
            TfsTeamProjectCollection teamProjectCollection = new TfsTeamProjectCollection(collectionUri, tfsCred);
            teamProjectCollection.Authenticate();
            return teamProjectCollection;
        }
    }
}
