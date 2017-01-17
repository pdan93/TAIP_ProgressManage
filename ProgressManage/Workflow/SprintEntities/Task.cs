using PetriNets;
using PetriNets.States;

namespace Workflow.SprintEntities
{
    public class Task : SprintEntity
    {
        public bool? Approved { get; set; }
        public float RemainingWork { get; set; }
        public string ResolvedState { get; set; }
        public string Reason { get; set; }
        public string WorkItemType { get; set; }

        public Task(State state) : base(state)
        {
        }

        public SprintEntity CreateSprintEntity(State state)
        {
            return new Task(state);
        }

        public override void Close()
        {
            if (State.GetType() != typeof(Tested)) return;
            State = new Done();
        }

        public override void MoveTo(State nextState)
        {
            if (State.GetType() == typeof(New) && nextState.GetType() == typeof(InProgress)) return;
            State = nextState;
        }
    }
}
