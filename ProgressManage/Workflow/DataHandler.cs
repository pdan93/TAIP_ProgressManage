using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Workflow
{
    public class DataHandler
    {
        public List<Revion> ImportTaskRevisions(string revionsFilePath)
        {
            using(var streamReader = new StreamReader(revionsFilePath))
            {
                string json = streamReader.ReadToEnd();
                string jsonWithoutSystem = json.Replace("System.", string.Empty);
                string jsonWithoutScheduling = jsonWithoutSystem.Replace("Microsoft.VSTS.Scheduling.", string.Empty);
                RevionGroup items = JsonConvert.DeserializeObject<RevionGroup>(jsonWithoutScheduling);
                return items.Value;
            }
        }
    }

    public class RevionGroup
    {
        public int Count { get; set; }
        public List<Revion> Value { get; set; }
    }

    public class Revion
    {
        public int Id { get; set; }
        public int Rev { get; set; }
        public Fields Fields { get; set; }
    }

    public class Fields
    {
        public string State { get; set; }
        public int RemainingWork { get; set; }
        public bool Tested { get; set; }
        public bool Implemented { get; set; }
    }
}
