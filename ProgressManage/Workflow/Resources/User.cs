using System;
using PetriNets;
using PetriNets.Observer;

namespace Workflow.Resources
{
    public class User : StateObserver
    {
        private string Name { get; set; }

        public User(SprintEntity sprintEntity) : base(sprintEntity)
        {
        }
    }
}