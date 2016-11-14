using PetriNets;
using PetriNets.States;

namespace Workflow
{
    public class Task : SprintEntity, ISprintEntityFactory
    {
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
