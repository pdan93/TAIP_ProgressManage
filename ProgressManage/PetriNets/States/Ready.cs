using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets
{
    public class Ready : State
    {
        public Ready()
        {
            Name = "Ready";
        }

        public override void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new Ready();
        }
    }
}
