using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using Newtonsoft.Json;
using PetriNets;
using PetriNets.States;
using Workflow.SprintEntities;

namespace Workflow
{
    public class Sprint : SprintPrototype
    {
        public IEnumerable<Story> Stories { get; set; }
        public PetriNetwork PetriNetwork;
        
        public List<State> States;

        public Sprint()
        {
            PetriNetwork = new PetriNetwork();
        }

        public void MoveDefectTo(Defect defect, State nextState)
        {
            var newMajorDefects = PetriNetwork.Entities.Where(entity => entity.GetType() == typeof(Defect)
                                && entity.State.GetType() == typeof(New)
                                && ((Defect)entity).Priority == PriorityEnum.Major);

            var isValidState = defect.Priority == PriorityEnum.Minor
                               && !newMajorDefects.Any()
                               && nextState.GetType() == typeof(InProgress);

            if (!isValidState) return;
            defect.State = nextState;
        }

        public override SprintPrototype Clone()
        {
            return (SprintPrototype)MemberwiseClone();
        }

        [LoggingAspect]
        public void CreateNetwork(SprintEntity sprintEntity, string transitionPath)
        {
            var statesDictionary = GetStatesDictionary();
            using (var streamReader = new StreamReader(transitionPath))
            {
                var transitions = JsonConvert.DeserializeObject<IEnumerable<TransitionJson>>(streamReader.ReadToEnd());
                foreach (var transitionData in transitions)
                {
                    var methodInfo = typeof(Preconditions).GetMethod(transitionData.Precondition);
                    var preconditionMethod = (Func<SprintEntity, bool>)Delegate.CreateDelegate(typeof(Func<SprintEntity, bool>), methodInfo);
                    var transition = new Transition(sprintEntity, preconditionMethod, statesDictionary[transitionData.NextState]);
                    statesDictionary[transitionData.PrevState].Transitions.Add(transition);
                }
            }
            States = new List<State>();
            States.AddRange(statesDictionary.Values );
        }

        private static Dictionary<string, State> GetStatesDictionary()
        {
            return typeof(StatesNames)
                .GetFields()
                .ToDictionary(field => field.Name, field => new State(field.GetValue(typeof(StatesNames)) as string));
        }

        [LoggingAspect]
        public string IsHistoryValid(Task task, IEnumerable<Revion> taskRevions)
        {
            task.State = States.SingleOrDefault(x => x.Name == StatesNames.Start);
            foreach (var taskHistory in taskRevions.Select(x=> x.Fields))
            {
                var nextState = task.State.GetValidTransition()?.NextState;
                var properties = taskHistory.GetType().GetProperties();

                if (nextState == null)
                {
                    return "The task history doesn't match the petri nets transitions from state: " + task.State.Name;
                }

                foreach (var property in properties)
                {
                    var taskProperty = task.GetType().GetProperty(property.Name);
                    if (taskProperty.Name == "State")
                    {
                        var stateName = (string)property.GetValue(taskHistory, null);
                        taskProperty.SetValue(task, States.SingleOrDefault(x => x.Name == stateName));
                    }
                    else
                    {
                        taskProperty.SetValue(task, property.GetValue(taskHistory, null));
                    }
                    
                }

                if (task.State.Name != StatesNames.End && nextState.Name != task.State.Name)
                {
                    return "The state should be " + nextState.Name + " but it is: " + task.State.Name;
                }
            }
            return "The History of the task passed the test";
        }

    }


    public class TransitionJson
    {
        public string Name { get; set; }
        public string PrevState { get; set; }
        public string NextState { get; set; }
        public string Precondition { get; set; }
    }
}
