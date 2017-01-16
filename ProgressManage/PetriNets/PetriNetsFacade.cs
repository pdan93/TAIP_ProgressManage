
namespace PetriNets
{
    public class PetriNetsFacade
    {
        private readonly PetriNetsEvaluator _petriNetsEvaluator;
        private readonly PetriNetwork _petriNetwork;

        public PetriNetsFacade()
        {
            _petriNetsEvaluator = PetriNetsEvaluator.Instance;
            _petriNetwork = new PetriNetwork();
        }

        public void InsertSprintEntity(SprintEntity entity)
        {
            _petriNetwork.InsertSprintEntity(entity);
        }

    }

}
