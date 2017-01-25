using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.ProcessConfiguration.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualStudio.Services.Common;

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


        public static void GetTeams(TfsConnection teamProjectCollection)
        {
            Uri _uri = teamProjectCollection.Uri;
            string user = ConfigurationManager.AppSettings["TfsClientUser"];
            string password = ConfigurationManager.AppSettings["TfsClientPassword"];
            VssBasicCredential _credentials = new VssBasicCredential(user, password);

            using (ProjectHttpClient projectHttpClient = new ProjectHttpClient(_uri, _credentials))
            {
                IEnumerable<TeamProjectReference> projects = projectHttpClient.GetProjects().Result;
                IEnumerable<WebApiTeam> teams = GetTeams(projects.First().Name, _credentials, _uri);

                var configSvc = teamProjectCollection.GetService<TeamSettingsConfigurationService>();
                var configs = configSvc.GetTeamConfigurations(teams.Select(x=> x.Id));
            }
        }

        public static IEnumerable<WebApiTeam> GetTeams(string _project, VssBasicCredential _credentials, Uri _uri)
        {
            using (var teamHttpClient = new TeamHttpClient(_uri, _credentials))
            {
                return teamHttpClient.GetTeamsAsync(_project).Result;
            }
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

    public class Project
    {
        public string Name { get; set; }
        public List<Team> Team { get; set; }
    }

    public class Team
    {
        public string Name { get; set; }
        public List<Iteration> Iterations { get; set; }
    }

    public class Iteration
    {
        public int TotalCapacity;
        public int Work;
        public int InitialEstimation;
    }


}