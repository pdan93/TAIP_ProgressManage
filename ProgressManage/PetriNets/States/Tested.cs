using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets.States
{
    public class Tested : State
    {
        public Tested()
        {
            Name = "Tested";
        }

        public override void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new Tested();
        }
    }
}
