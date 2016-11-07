using PetriNets;

namespace Workflow.SprintEntities
{
    public class Defect : SprintEntity, ISprintEntityFactory
    {
        public Defect(State state) : base(state)
        {
        }

        public SprintEntity CreateSprintEntity(State state)
        {
            return new Defect(state);
        }
    }
}
