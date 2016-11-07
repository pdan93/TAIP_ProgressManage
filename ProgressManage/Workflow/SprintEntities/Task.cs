using PetriNets;

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
    }
}
