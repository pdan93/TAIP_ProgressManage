using PetriNets;
using Workflow.SprintEntities;

namespace Workflow
{
    public static class StatesDictionary
    {
        public static string Start = "start";
        public static string EstimationStatus = "estimationStatus";
        public static string AddToImplementation = "addToImplementation";
        public static string ImplementationDone = "implementationDone";
        public static string TestingStatus = "testingStatus";
        public static string End = "end";
    }

    public static class Preconditions
    {
        public static bool Sort(SprintEntity entity)
        {
            return true;
        }

        public static bool SendBugToImplementation(SprintEntity entity)
        {
            return entity.GetType() == typeof(Defect) 
                && entity.State.Name == StatesDictionary.Start;
        }

        public static bool Estimate(SprintEntity entity)
        {
            return entity.GetType() == typeof(Task) 
                && entity.State.Name == StatesDictionary.Start;
        }

        public static bool SendTaskToImplementation(SprintEntity entity)
        {
            var task = entity as Task;
            if (entity.GetType() != typeof(Task) && task.Approved.HasValue && task.Approved == false)
                return false;
            return task.State.Name == StatesDictionary.EstimationStatus
                   && task.Approved.GetValueOrDefault()
                   && task.Estimation > 0;
        }

        public static bool CloseTask(SprintEntity entity)
        {
            if (entity.GetType() != typeof(Task)) return false;
            var task = entity as Task;
            return task.State.Name == StatesDictionary.EstimationStatus
                && task.Approved.HasValue 
                && task.Approved == false;
        }

        public static bool StartImplementation(SprintEntity entity)
        {
            if (entity.GetType() == typeof(Task)) 
                return (entity as Task).Approved.GetValueOrDefault();
            if (entity.GetType() == typeof(Defect))
                return (entity as Defect).Priority == PriorityEnum.Major;
            return false;
        }

        public static bool StartTesting(SprintEntity entity)
        {
            return !entity.Tested.HasValue
                && entity.Implemented;
        }

        public static bool SendBackToImplementation(SprintEntity entity)
        {
            return entity.Tested.HasValue 
                && entity.Tested == false;
        }

        public static bool CloseTaskAfterImplementation(SprintEntity entity)
        {
            return entity.Tested.GetValueOrDefault();
        }
    }
}
