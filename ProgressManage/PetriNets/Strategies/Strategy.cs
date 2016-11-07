using System.Collections.Generic;

namespace PetriNets.Strategies
{
    public abstract class Strategy
    {
        public abstract IEnumerable<string> Run(PetriNetwork petriNetwork, SprintEntity token);
    }
}
