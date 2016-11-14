using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PetriNets
{
    public class New : State
    {
        public New()
        {
            Name = "New";
        }

        public override void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new New();
        }
    }
}
