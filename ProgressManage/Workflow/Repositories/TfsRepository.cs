using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Workflow.Repositories
{
    public class TfsRepository
    {
        public static IEnumerable<WorkItem> GetWorkItems(TfsConnection teamProjectCollection)
        {
            WorkItemStore workItemStore = teamProjectCollection.GetService<WorkItemStore>();
            WorkItemCollection workItemCollection = workItemStore.Query(
                "SELECT [System.Id], [System.WorkItemType]," +
                "[System.State], [System.AssignedTo]," +
                "[System.Title] FROM WorkItems");
            return workItemCollection
                .OfType<WorkItem>();
        }

        public static TfsTeamProjectCollection GetTeamProjectCollection(Uri collectionUri, out NetworkCredential credential)
        {
            string user = ConfigurationManager.AppSettings["TfsClientUser"];
            string password = ConfigurationManager.AppSettings["TfsClientPassword"];
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