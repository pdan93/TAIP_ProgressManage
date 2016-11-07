using PetriNets;

namespace Workflow
{
    interface ISprintEntityFactory
    {
        SprintEntity CreateSprintEntity(State state);
    }
}

