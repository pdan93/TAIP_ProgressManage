using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public void CreateNetwork(SprintEntity entity)
        {
            State start = new State(StatesDictionary.Start);
            State estimationStatus = new State(StatesDictionary.EstimationStatus);
            State addToImplementation = new State(StatesDictionary.AddToImplementation);
            State implementationDone = new State(StatesDictionary.ImplementationDone);
            State testingStatus = new State(StatesDictionary.TestingStatus);
            State end = new State(StatesDictionary.End);

            Transition estimateTransition = new Transition(entity, Preconditions.Estimate, estimationStatus);
            Transition closeTaskTransition = new Transition(entity, Preconditions.CloseTask, end);
            Transition sendTaskToImplementationTransition = new Transition(entity, Preconditions.SendTaskToImplementation, addToImplementation);
            Transition sendBugToImplementationTransition = new Transition(entity, Preconditions.SendBugToImplementation, addToImplementation);
            Transition startImplementationTransition = new Transition(entity, Preconditions.StartImplementation, implementationDone);
            Transition startTestingTransition = new Transition(entity, Preconditions.StartTesting, testingStatus);
            Transition sendBackToImplementationTransition = new Transition(entity, Preconditions.SendBackToImplementation, addToImplementation);
            Transition closeTaskAfterImplementationTransition = new Transition(entity, Preconditions.CloseTaskAfterImplementation, end);

            start.Transitions.Add(estimateTransition);
            start.Transitions.Add(sendBugToImplementationTransition);
            estimationStatus.Transitions.Add(closeTaskTransition);
            estimationStatus.Transitions.Add(sendTaskToImplementationTransition);
            addToImplementation.Transitions.Add(startImplementationTransition);
            implementationDone.Transitions.Add(startTestingTransition);
            testingStatus.Transitions.Add(closeTaskAfterImplementationTransition);
            testingStatus.Transitions.Add(sendBackToImplementationTransition);

            States = new List<State>() { start, estimationStatus, addToImplementation, implementationDone, testingStatus, end };
        }

        [LoggingAspect]
        public string IsHistoryValid(Task task, IEnumerable<Revion> taskRevions)
        {
            task.State = States.SingleOrDefault(x => x.Name == StatesDictionary.Start);
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



                if (task.State.Name != StatesDictionary.End && nextState.Name != task.State.Name)
                {
                    return "The state should be " + nextState.Name + " but it is: " + task.State.Name;
                }
            }
            return "The History of the task passed the test";
        }

    }

}
