using PetriNets;
using PetriNets.States;

namespace Workflow.SprintEntities
{
    public enum PriorityEnum
    {
        Major,
        Minor
    }

    public class Defect : SprintEntity, ISprintEntityFactory
    {
        public PriorityEnum Priority { get; set; }
        public Defect(State state) : base(state)
        {
        }

        public SprintEntity CreateSprintEntity(State state)
        {
            return new Defect(state);
        }

        public override void Close()
        {
            State = new Fixed();
        }

        public override void MoveTo(State nextState)
        {
            State = nextState;
        }
    }
}
