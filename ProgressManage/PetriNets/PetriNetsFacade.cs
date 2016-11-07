using System.Collections.Generic;
using PetriNets.Strategies;

namespace PetriNets
{
    public class PetriNetsFacade
    {
        private readonly PetriNetsEvaluator _petriNetsEvaluator;
        private readonly PetriNetwork _petriNetwork;
        private readonly Strategy _strategy;

        public PetriNetsFacade()
        {
            _petriNetsEvaluator = PetriNetsEvaluator.Instance;
            _petriNetwork = new PetriNetwork();
            _strategy = new TaskStrategy();
        }

        public void InsertSprintEntity(SprintEntity entity)
        {
            _petriNetwork.InsertSprintEntity(entity);
        }

        public void Evaluate(IEnumerable<SprintEntity> states)
        {
            _petriNetsEvaluator.EvaluateStates(states);
        }

        public void Run(SprintEntity token)
        {
            _strategy.Run(_petriNetwork, token);
        }
    }

}
