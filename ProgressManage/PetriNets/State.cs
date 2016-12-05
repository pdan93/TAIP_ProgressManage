using System.Collections.Generic;
using System.Linq;

namespace PetriNets
{
    public class State
    {
        public string Name { get; set; }
        public List<Transition> Transitions { get; set; }

        public State() { }

        public State(string name)
        {
            Name = name;
            Transitions = new List<Transition>();
        }

        public void UseTransition()
        {
            var validTransition = GetValidTransition();
            validTransition?.MoveToNext();
        }

        public Transition GetValidTransition()
        {
            return Transitions.FirstOrDefault(transtion => transtion.IsApplicable());
        }
    }
}
