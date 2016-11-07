using System.Collections.Generic;
using PetriNets;
using Workflow.SprintEntities;

namespace Workflow
{
    public class Story : SprintEntity, ISprintEntityFactory
    {
        public List<Task> Tasks;
        public List<Defect> Defects;

        public Story(State state) : base(state)
        {
        }

        public SprintEntity CreateSprintEntity(State state)
        {
           return new Story(state);
        }
    }
}
