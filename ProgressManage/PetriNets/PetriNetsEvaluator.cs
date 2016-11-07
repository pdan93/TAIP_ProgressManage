
using System.Collections.Generic;
using System.Linq;
using PetriNets.Strategies;

namespace PetriNets
{
    public sealed class PetriNetsEvaluator
    {
        public static PetriNetsEvaluator Instance { get; } = new PetriNetsEvaluator();

        static PetriNetsEvaluator() { }
        private PetriNetsEvaluator() {}

        public IEnumerable<string> EvaluateStates(IEnumerable<SprintEntity> entities)
        {
            return entities.Select(entity => entity.State.Name);
        }
    }
}
