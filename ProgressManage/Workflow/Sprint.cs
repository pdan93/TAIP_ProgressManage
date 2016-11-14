using System.Collections.Generic;
using System.Linq;
using PetriNets;
using Workflow.SprintEntities;

namespace Workflow
{
    public class Sprint : SprintPrototype
    {
        public IEnumerable<Story> Stories { get; set; }
        public PetriNetwork PetriNetwork;

        public Sprint()
        {
            PetriNetwork = new PetriNetwork();
        }

        public void MoveDefectTo(Defect defect, State nextState)
        {
            var newMajorDefects = PetriNetwork.Entities.Where(entity => entity.GetType() == typeof(Defect)
                                && entity.State.GetType() == typeof(New)
                                && ((Defect)entity).Priority == PriorityEnum.Major);

            var isValidState = defect.Priority == PriorityEnum.Minor
                               && !newMajorDefects.Any()
                               && nextState.GetType() == typeof(InProgress);

            if (!isValidState) return;
            defect.State = nextState;
        }

        public override SprintPrototype Clone()
        {
            return (SprintPrototype)MemberwiseClone();
        }
    }

}
