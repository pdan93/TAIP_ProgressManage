using PetriNets;

namespace Workflow.SprintEntities
{
    interface ISprintEntityFactory
    {
        SprintEntity CreateSprintEntity(State state);
    }
}

