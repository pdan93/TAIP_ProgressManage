using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets.States
{
    public class Fixed : State
    {
        public Fixed()
        {
            Name = "Fixed";
        }

        public override void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new Done();
        }
    }
}
