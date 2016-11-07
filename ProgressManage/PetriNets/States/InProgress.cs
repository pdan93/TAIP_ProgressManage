using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets
{
    public class InProgress : State
    {
        public override void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new InProgress();
        }
    }
}
