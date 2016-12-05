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


        public bool IsHistoryValid(Task task)
        {
            task.State = States.SingleOrDefault(x => x.Name == StatesDictionary.Start);
            foreach (var taskHistory in GetTaskHistory())
            {
                var nextState = task.State.GetValidTransition()?.NextState;
                var properties = taskHistory.GetType().GetProperties();

                foreach (var property in properties)
                {
                    var taskProperty = task.GetType().GetProperty(property.Name);
                    taskProperty.SetValue(task, property.GetValue(taskHistory, null));
                }

                if (task.State.Name != StatesDictionary.End && nextState.Name != task.State.Name)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<object> GetTaskHistory()
        {
            yield return new
            {
                State = States.SingleOrDefault(x => x.Name == StatesDictionary.EstimationStatus),
                Approved = true,
                Estimation = 5
            };

            yield return new
            {
                State = States.SingleOrDefault(x => x.Name == StatesDictionary.AddToImplementation),
            };

            yield return new
            {
                State = States.SingleOrDefault(x => x.Name == StatesDictionary.ImplementationDone),
                Implemented = true
            };

            yield return new
            {
                State = States.SingleOrDefault(x => x.Name == StatesDictionary.TestingStatus),
                Tested = true
            };

            yield return new
            {
                State = States.SingleOrDefault(x => x.Name == StatesDictionary.End),
                ResolvedState = "done"
            };
        }


        public bool ValidateHistoryOfEntity(List<SprintEntity> entities)
        {
            return false;
        }
    }

}
