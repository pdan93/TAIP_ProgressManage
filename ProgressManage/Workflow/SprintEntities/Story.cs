using System.Collections.Generic;
using PetriNets;

namespace Workflow.SprintEntities
{
    public class Story : SprintEntity
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
